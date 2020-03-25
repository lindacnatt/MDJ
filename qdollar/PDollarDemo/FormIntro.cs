using System;
using System.Windows.Forms;

namespace PDollarDemo
{
    public partial class FormIntro : Form
    {
        public FormIntro()
        {
            InitializeComponent();
        }

        private void FormIntro_Load(object sender, EventArgs e)
        {
            richTextBox.Text = @"Demo Application for the $P, $P+, and $Q Point-Cloud Recognizers 
 
    Radu-Daniel Vatavu, Ph.D.
    University Stefan cel Mare of Suceava
    Suceava 720229, Romania
    radu.vatavu@usm.ro
 
    Lisa Anthony, Ph.D.
    Department of CISE
    University of Florida
    Gainesville, FL 32611, USA
    lanthony@cise.ufl.edu
 
    Jacob O. Wobbrock, Ph.D.
    The Information School
    University of Washington
    Seattle, WA 98195-2840
    wobbrock@uw.edu
 
The academic publications for the $P, $P+, and $Q recognizers, and what should be used to cite them, are:
  	
    Vatavu, R.-D., Anthony, L. and Wobbrock, J.O. (2018).  
    $Q: A Super-Quick, Articulation-Invariant Stroke-Gesture
    Recognizer for Low-Resource Devices. Proceedings of 20th International Conference on
    Human-Computer Interaction with Mobile Devices and Services (MobileHCI '18). 
    Barcelona, Spain (September 3-6, 2018). New York: ACM Press.
    https://doi.org/10.1145/3229434.3229465

    Vatavu, R.-D. (2017). 
    Improving Gesture Recognition Accuracy on Touch Screens for Users with Low Vision.
    Proceedings of the 35th ACM Conference on Human Factors in Computing Systems (CHI '17).
    Denver, Colorado (May 2017). New York: ACM Press.
    http://dx.doi.org/10.1145/3025453.3025941

    Vatavu, R.-D., Anthony, L. and Wobbrock, J.O. (2012). 
    Gestures as point clouds: A $P recognizer for user interface prototypes. 
    Proceedings of the ACM International Conference on Multimodal Interfaces (ICMI '12). 
    Santa Monica, California (October 22-26, 2012). New York: ACM Press, pp. 273-280.
    https://doi.org/10.1145/2388676.2388732
 
This software is distributed under the ""New BSD License"" agreement:
 
Copyright (c) 2012-2018, Radu-Daniel Vatavu, Lisa Anthony, and Jacob O. Wobbrock. All rights reserved.
 
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
- Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
- Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
- Neither the names of the University Stefan cel Mare of Suceava, University of Washington, nor University of Florida, nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
 
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS ""AS IS"" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL Radu-Daniel Vatavu OR Lisa Anthony
OR Jacob O. Wobbrock BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT 
OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
SUCH DAMAGE.";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormMain()).ShowDialog();
            this.Close();
        }
    }
}
