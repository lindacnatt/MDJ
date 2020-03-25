using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using PDollarGestureRecognizer;

namespace PDollarDemo
{
    /// <summary>
    /// 
    /// Implements classification experiment (user-independent training)
    /// 
    /// </summary>
    public class Experiment_ClassificationTime
    {
        public delegate string GestureRecognizer(Gesture candidate, Gesture[] templateSet);

        // experiment design
        private Gesture[] gestures;
        private int numGestureClasses = 0;
        private GestureRecognizer[] RECOGNIZERS;
        private int[] NUM_TRAINING_SAMPLES;
        private int REPETITIONS;
        private const int NUM_REPEATED_MEASUREMENTS = 10;

        /// <summary>
        /// Creates a classification time experiment for a gesture set, a set of recognizers, various number of training samples per gesture type,
        /// and the number of repetitions of each experiment condition.
        /// </summary>
        public Experiment_ClassificationTime(Gesture[] gestures, GestureRecognizer[] recognizers, int[] numTrainingSamples, int repetitions = 100)
        {
            this.gestures = gestures;
            numGestureClasses = gestures.Select(g => g.Name).Distinct().Count();
            this.RECOGNIZERS = recognizers;
            this.NUM_TRAINING_SAMPLES = numTrainingSamples;
            this.REPETITIONS = repetitions;
        }

        /// <summary>
        /// Runs the experiment procedure and logs results to a CSV file.
        /// </summary>
        public void RunExperiment(string outputFileName, bool verbose = true)
        {
            Console.WriteLine("This procedure needs at least two samples of each gesture type to be present in the dataset.\r\n");
            Console.WriteLine("Average classification times are shown for each recognizer and each number of samples per gesture type (T). User-independent recognition rates are computed.\r\n");

            Array.Sort(gestures, this.CompareGestures);
            if (verbose)
                Console.WriteLine("Gestures loaded, start experiment...\r\n");

            using (StreamWriter writer = new StreamWriter(outputFileName))
            {
                writer.WriteLine("Recognizer,T,TrainingSetSize,Trial,TimeInMs");

                foreach (int T in NUM_TRAINING_SAMPLES)
                {
                    double[] avgTime = new double[RECOGNIZERS.Length];
                    Array.Clear(avgTime, 0, avgTime.Length);
                    double[] accuracy = new double[RECOGNIZERS.Length];
                    Array.Clear(accuracy, 0, accuracy.Length);
                    double[] numTests = new double[RECOGNIZERS.Length];
                    Array.Clear(numTests, 0, numTests.Length);
                        
                    for (int indexRepetition = 0; indexRepetition < REPETITIONS; indexRepetition++) // repeat classification multiple times with various randomly selected configurations of the training set
                    {
                        // select T samples for training and 1 sample for testing for each gesture type
                        int[] training = null, testing = null;
                        UserDependent_BuildTrainingAndTestingSets(gestures, numGestureClasses, T, out training, out testing);
                        Randomize(training);
                        Gesture[] trainingSet = new Gesture[training.Length];
                        for (int i = 0; i < training.Length; i++)
                            trainingSet[i] = gestures[training[i]];

                        // run classification and measure execution times
                        double[] times = new double[RECOGNIZERS.Length];
                        for (int indexRecognizer = 0; indexRecognizer < RECOGNIZERS.Length; indexRecognizer++)
                        {
                            Stopwatch watch = new Stopwatch();
                            watch.Start();
                            for (int indexR = 0; indexR < NUM_REPEATED_MEASUREMENTS; indexR++) // repeat classifications a number of times in case one run is too fast to measure time accurately
                            {
                                for (int i = 0; i < testing.Length; i++)
                                {
                                    // normalize the gesture candidate
                                    gestures[testing[i]].Normalize(
                                        (RECOGNIZERS[indexRecognizer].Method.DeclaringType.Name.IndexOf("QPointCloud") >= 0) ? true : false // if $Q, compute the LUT as part of the normalization procedure
                                    );

                                    // compute classification result
                                    string predictedClass = RECOGNIZERS[indexRecognizer](gestures[testing[i]], trainingSet);
                                    if (predictedClass.CompareTo(gestures[testing[i]].Name) == 0)
                                        accuracy[indexRecognizer]++;
                                    numTests[indexRecognizer]++;
                                }
                            }
                            watch.Stop();
                            times[indexRecognizer] = watch.ElapsedTicks * 1000.0 / Stopwatch.Frequency / testing.Length / NUM_REPEATED_MEASUREMENTS;
                            avgTime[indexRecognizer] += times[indexRecognizer];
                        }

                        // write to file
                        for (int indexRecognizer = 0; indexRecognizer < RECOGNIZERS.Length; indexRecognizer++)
                            writer.WriteLine("{0},{1},{2},{3},{4:.00}",
                                indexRecognizer + 1,
                                T,
                                trainingSet.Length,
                                indexRepetition,
                                times[indexRecognizer]
                            );
                    }

                    if (verbose)
                    {
                        Console.WriteLine("T = {0} sample{1} per gesture type | {2} total samples", T, (T > 1) ? "s": "", this.numGestureClasses * T);
                        for (int indexRecognizer = 0; indexRecognizer < RECOGNIZERS.Length; indexRecognizer++)
                            Console.WriteLine("\t{0,25}:    Avg. time {1,5:.00} ms | Accuracy {2:.00}%",
                                RECOGNIZERS[indexRecognizer].Method.DeclaringType.Name,
                                avgTime[indexRecognizer] / REPETITIONS,
                                accuracy[indexRecognizer] / numTests[indexRecognizer] * 100.0
                            );
                        
                        Console.WriteLine(" $Q was {0:.0}x faster than $P on this CPU ({1:.0}% gain) for T={2} samples per gesture type.", 
                            avgTime[0] / avgTime[1],
                            (1.0 - avgTime[1] / avgTime[0]) * 100.0,
                            T
                        );
                        Console.WriteLine();
                    }
                }
            }

            Console.WriteLine("Experiment completed.");
        }

        /// <summary>
        /// Splits a gesture set into training and testing
        /// (T samples for each gesture type will be selected for training and 1 sample for testing).
        /// Assumes that the gestures[...] array is ordered by gesture type.
        /// </summary>
        /// <param name="gestures"></param>
        /// <param name="T"></param>
        /// <param name="indexesTraining"></param>
        /// <param name="indexesTesting"></param>
        public static void UserDependent_BuildTrainingAndTestingSets(
            Gesture[] gestures, int numClasses, int T,
            out int[] indexesTraining, out int[] indexesTesting)
        {
            indexesTraining = new int[numClasses * T];
            int numTraining = 0;
            indexesTesting = new int[numClasses];
            int numTesting = 0;

            int startIndex = 0;
            for (int i = 1; i <= gestures.Length; i++)
                if (i == gestures.Length || gestures[i].Name.CompareTo(gestures[i - 1].Name) != 0)
                {
                    int[] indexes = SelectRandomIntegers(T + 1, startIndex, i);
                    for (int j = 0; j < T; j++)
                        indexesTraining[numTraining++] = indexes[j];
                    indexesTesting[numTesting++] = indexes[T];
                    startIndex = i;
                }
        }

        /// <summary>
        /// Generates T integers randomly in the range [inf,sup)
        /// </summary>
        /// <param name="T">Number of distinct integers to generate</param>
        /// <param name="inf">Lower limit of the interval (inclusive)</param>
        /// <param name="sup">Upper limit (exclusive)</param>
        /// <returns></returns>
        private static Random random = new Random((int)DateTime.Now.Ticks);
        public static int[] SelectRandomIntegers(int T, int inf, int sup)
        {
            int[] values = new int[T];
            for (int i = 0; i < T; i++)
            {
                bool ok;
                int v;
                do
                {
                    v = random.Next(inf, sup);
                    ok = true;
                    for (int j = 0; j < i; j++)
                        if (values[j] == v)
                        {
                            ok = false;
                            break;
                        }
                } while (!ok);
                values[i] = v;
            }
            return values;
        }

        private void Randomize(int[] training)
        {
            for (int i = 0; i < training.Length * 10; i++)
            {
                int index1 = random.Next(training.Length);
                int index2 = random.Next(training.Length);
                int temp = training[index1];
                training[index1] = training[index2];
                training[index2] = temp;
            }
        }

        private int CompareGestures(Gesture a, Gesture b)
        {
            return a.Name.CompareTo(b.Name);
        }
    }
}
