﻿namespace YBOT_Field_Control_2016
{
    partial class Score
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOverride = new System.Windows.Forms.Button();
            this.btnUpdateScore = new System.Windows.Forms.Button();
            this.grRedFinalScore = new System.Windows.Forms.GroupBox();
            this.tbRedPenalty = new System.Windows.Forms.TextBox();
            this.lbRedPenaltyPointValue = new System.Windows.Forms.Label();
            this.lbRedPenaltyScore = new System.Windows.Forms.Label();
            this.btnRedDq = new System.Windows.Forms.Button();
            this.lblRedDQ = new System.Windows.Forms.Label();
            this.lblRedPenalty = new System.Windows.Forms.Label();
            this.lblRedFinalScore = new System.Windows.Forms.Label();
            this.gbGreenFinalScore = new System.Windows.Forms.GroupBox();
            this.tbGreenPenalty = new System.Windows.Forms.TextBox();
            this.lbGreenPenaltyPointValue = new System.Windows.Forms.Label();
            this.lbGreenPenaltyScore = new System.Windows.Forms.Label();
            this.btnGreenDq = new System.Windows.Forms.Button();
            this.lblGreenDq = new System.Windows.Forms.Label();
            this.lblGreenPenalty = new System.Windows.Forms.Label();
            this.lblGreenFinalScore = new System.Windows.Forms.Label();
            this.btnFinalScore = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbEmergencyCycledPenalty = new System.Windows.Forms.ComboBox();
            this.tbRockWeight = new System.Windows.Forms.TextBox();
            this.tbManualEmergencyCycled = new System.Windows.Forms.TextBox();
            this.tbManualSolar2Score = new System.Windows.Forms.TextBox();
            this.tbManualSolar1Score = new System.Windows.Forms.TextBox();
            this.tbAutoSolarScore = new System.Windows.Forms.TextBox();
            this.tbAutoEmergencyCycled = new System.Windows.Forms.TextBox();
            this.tbAutoCornersTested = new System.Windows.Forms.TextBox();
            this.lbRocketLaunchedPointValue = new System.Windows.Forms.Label();
            this.lbManualEmergencyCycledPointValue = new System.Windows.Forms.Label();
            this.lbAutoEmergencyCycledPointValue = new System.Windows.Forms.Label();
            this.lbAutoCornersTestedPointValue = new System.Windows.Forms.Label();
            this.lbRocketLaunchedScore = new System.Windows.Forms.Label();
            this.lbManualEmergencyCycledScore = new System.Windows.Forms.Label();
            this.lbRocketPositionMulitplier = new System.Windows.Forms.Label();
            this.lbRockScore = new System.Windows.Forms.Label();
            this.lbAutoEmergencyCycledScore = new System.Windows.Forms.Label();
            this.lbAutoCornerTestedScore = new System.Windows.Forms.Label();
            this.cbRocketLaunched = new System.Windows.Forms.CheckBox();
            this.cbRocketPosition = new System.Windows.Forms.ComboBox();
            this.lbJointScoreLabel = new System.Windows.Forms.Label();
            this.lbRocketLaunched = new System.Windows.Forms.Label();
            this.lbRockWeight = new System.Windows.Forms.Label();
            this.lbRocketPosition = new System.Windows.Forms.Label();
            this.lbEmergencyPenalty = new System.Windows.Forms.Label();
            this.lbManualEmergencyCycled = new System.Windows.Forms.Label();
            this.lbManualSolar2 = new System.Windows.Forms.Label();
            this.lbManualSolar1 = new System.Windows.Forms.Label();
            this.lbAutoSolar = new System.Windows.Forms.Label();
            this.lbAutoEmergencyCycled = new System.Windows.Forms.Label();
            this.lbAutoCornersTested = new System.Windows.Forms.Label();
            this.lbJointScore = new System.Windows.Forms.Label();
            this.lbGreenScore = new System.Windows.Forms.Label();
            this.lbRedScore = new System.Windows.Forms.Label();
            this.grRedFinalScore.SuspendLayout();
            this.gbGreenFinalScore.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOverride
            // 
            this.btnOverride.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOverride.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOverride.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOverride.Location = new System.Drawing.Point(265, 596);
            this.btnOverride.Margin = new System.Windows.Forms.Padding(10);
            this.btnOverride.Name = "btnOverride";
            this.btnOverride.Size = new System.Drawing.Size(200, 40);
            this.btnOverride.TabIndex = 132;
            this.btnOverride.Text = "Manual Override";
            this.btnOverride.UseVisualStyleBackColor = true;
            this.btnOverride.Click += new System.EventHandler(this.btnOverride_Click);
            // 
            // btnUpdateScore
            // 
            this.btnUpdateScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpdateScore.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnUpdateScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateScore.Location = new System.Drawing.Point(45, 596);
            this.btnUpdateScore.Margin = new System.Windows.Forms.Padding(10);
            this.btnUpdateScore.Name = "btnUpdateScore";
            this.btnUpdateScore.Size = new System.Drawing.Size(200, 40);
            this.btnUpdateScore.TabIndex = 131;
            this.btnUpdateScore.Text = "Update Score";
            this.btnUpdateScore.UseVisualStyleBackColor = true;
            this.btnUpdateScore.Click += new System.EventHandler(this.btnUpdateScore_Click);
            // 
            // grRedFinalScore
            // 
            this.grRedFinalScore.Controls.Add(this.lbRedScore);
            this.grRedFinalScore.Controls.Add(this.tbRedPenalty);
            this.grRedFinalScore.Controls.Add(this.lbRedPenaltyPointValue);
            this.grRedFinalScore.Controls.Add(this.lbRedPenaltyScore);
            this.grRedFinalScore.Controls.Add(this.btnRedDq);
            this.grRedFinalScore.Controls.Add(this.lblRedDQ);
            this.grRedFinalScore.Controls.Add(this.lblRedPenalty);
            this.grRedFinalScore.Controls.Add(this.lblRedFinalScore);
            this.grRedFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grRedFinalScore.ForeColor = System.Drawing.Color.Maroon;
            this.grRedFinalScore.Location = new System.Drawing.Point(370, 442);
            this.grRedFinalScore.Name = "grRedFinalScore";
            this.grRedFinalScore.Size = new System.Drawing.Size(316, 141);
            this.grRedFinalScore.TabIndex = 130;
            this.grRedFinalScore.TabStop = false;
            this.grRedFinalScore.Text = "Red Final Score";
            // 
            // tbRedPenalty
            // 
            this.tbRedPenalty.Enabled = false;
            this.tbRedPenalty.Location = new System.Drawing.Point(116, 31);
            this.tbRedPenalty.Name = "tbRedPenalty";
            this.tbRedPenalty.Size = new System.Drawing.Size(67, 31);
            this.tbRedPenalty.TabIndex = 185;
            this.tbRedPenalty.Tag = "[0,3]";
            this.tbRedPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRedPenalty.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            // 
            // lbRedPenaltyPointValue
            // 
            this.lbRedPenaltyPointValue.AutoSize = true;
            this.lbRedPenaltyPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRedPenaltyPointValue.ForeColor = System.Drawing.Color.Red;
            this.lbRedPenaltyPointValue.Location = new System.Drawing.Point(189, 35);
            this.lbRedPenaltyPointValue.Name = "lbRedPenaltyPointValue";
            this.lbRedPenaltyPointValue.Size = new System.Drawing.Size(43, 24);
            this.lbRedPenaltyPointValue.TabIndex = 184;
            this.lbRedPenaltyPointValue.Text = "200";
            this.lbRedPenaltyPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbRedPenaltyScore
            // 
            this.lbRedPenaltyScore.AutoSize = true;
            this.lbRedPenaltyScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRedPenaltyScore.ForeColor = System.Drawing.Color.Red;
            this.lbRedPenaltyScore.Location = new System.Drawing.Point(239, 35);
            this.lbRedPenaltyScore.Name = "lbRedPenaltyScore";
            this.lbRedPenaltyScore.Size = new System.Drawing.Size(21, 24);
            this.lbRedPenaltyScore.TabIndex = 177;
            this.lbRedPenaltyScore.Text = "0";
            this.lbRedPenaltyScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRedDq
            // 
            this.btnRedDq.Enabled = false;
            this.btnRedDq.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRedDq.Location = new System.Drawing.Point(243, 66);
            this.btnRedDq.Name = "btnRedDq";
            this.btnRedDq.Size = new System.Drawing.Size(67, 31);
            this.btnRedDq.TabIndex = 136;
            this.btnRedDq.UseVisualStyleBackColor = true;
            this.btnRedDq.Click += new System.EventHandler(this.btnRedDq_Click);
            // 
            // lblRedDQ
            // 
            this.lblRedDQ.AutoSize = true;
            this.lblRedDQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedDQ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRedDQ.Location = new System.Drawing.Point(7, 70);
            this.lblRedDQ.Name = "lblRedDQ";
            this.lblRedDQ.Size = new System.Drawing.Size(84, 24);
            this.lblRedDQ.TabIndex = 135;
            this.lblRedDQ.Text = "Red DQ";
            this.lblRedDQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRedPenalty
            // 
            this.lblRedPenalty.AutoSize = true;
            this.lblRedPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPenalty.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRedPenalty.Location = new System.Drawing.Point(7, 35);
            this.lblRedPenalty.Name = "lblRedPenalty";
            this.lblRedPenalty.Size = new System.Drawing.Size(95, 24);
            this.lblRedPenalty.TabIndex = 133;
            this.lblRedPenalty.Text = "Penalties";
            this.lblRedPenalty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRedFinalScore
            // 
            this.lblRedFinalScore.AutoSize = true;
            this.lblRedFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedFinalScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRedFinalScore.Location = new System.Drawing.Point(7, 105);
            this.lblRedFinalScore.Name = "lblRedFinalScore";
            this.lblRedFinalScore.Size = new System.Drawing.Size(117, 24);
            this.lblRedFinalScore.TabIndex = 5;
            this.lblRedFinalScore.Text = "Final Score";
            this.lblRedFinalScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbGreenFinalScore
            // 
            this.gbGreenFinalScore.Controls.Add(this.lbGreenScore);
            this.gbGreenFinalScore.Controls.Add(this.tbGreenPenalty);
            this.gbGreenFinalScore.Controls.Add(this.lbGreenPenaltyPointValue);
            this.gbGreenFinalScore.Controls.Add(this.lbGreenPenaltyScore);
            this.gbGreenFinalScore.Controls.Add(this.btnGreenDq);
            this.gbGreenFinalScore.Controls.Add(this.lblGreenDq);
            this.gbGreenFinalScore.Controls.Add(this.lblGreenPenalty);
            this.gbGreenFinalScore.Controls.Add(this.lblGreenFinalScore);
            this.gbGreenFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGreenFinalScore.ForeColor = System.Drawing.Color.Lime;
            this.gbGreenFinalScore.Location = new System.Drawing.Point(45, 442);
            this.gbGreenFinalScore.Name = "gbGreenFinalScore";
            this.gbGreenFinalScore.Size = new System.Drawing.Size(316, 141);
            this.gbGreenFinalScore.TabIndex = 129;
            this.gbGreenFinalScore.TabStop = false;
            this.gbGreenFinalScore.Text = "Green Final Score";
            // 
            // tbGreenPenalty
            // 
            this.tbGreenPenalty.Enabled = false;
            this.tbGreenPenalty.Location = new System.Drawing.Point(116, 31);
            this.tbGreenPenalty.Name = "tbGreenPenalty";
            this.tbGreenPenalty.Size = new System.Drawing.Size(67, 31);
            this.tbGreenPenalty.TabIndex = 184;
            this.tbGreenPenalty.Tag = "[0,3]";
            this.tbGreenPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbGreenPenalty.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            // 
            // lbGreenPenaltyPointValue
            // 
            this.lbGreenPenaltyPointValue.AutoSize = true;
            this.lbGreenPenaltyPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreenPenaltyPointValue.ForeColor = System.Drawing.Color.Red;
            this.lbGreenPenaltyPointValue.Location = new System.Drawing.Point(189, 35);
            this.lbGreenPenaltyPointValue.Name = "lbGreenPenaltyPointValue";
            this.lbGreenPenaltyPointValue.Size = new System.Drawing.Size(43, 24);
            this.lbGreenPenaltyPointValue.TabIndex = 183;
            this.lbGreenPenaltyPointValue.Text = "200";
            this.lbGreenPenaltyPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbGreenPenaltyScore
            // 
            this.lbGreenPenaltyScore.AutoSize = true;
            this.lbGreenPenaltyScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreenPenaltyScore.ForeColor = System.Drawing.Color.Red;
            this.lbGreenPenaltyScore.Location = new System.Drawing.Point(239, 35);
            this.lbGreenPenaltyScore.Name = "lbGreenPenaltyScore";
            this.lbGreenPenaltyScore.Size = new System.Drawing.Size(21, 24);
            this.lbGreenPenaltyScore.TabIndex = 176;
            this.lbGreenPenaltyScore.Text = "0";
            this.lbGreenPenaltyScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGreenDq
            // 
            this.btnGreenDq.Enabled = false;
            this.btnGreenDq.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGreenDq.Location = new System.Drawing.Point(243, 66);
            this.btnGreenDq.Name = "btnGreenDq";
            this.btnGreenDq.Size = new System.Drawing.Size(67, 31);
            this.btnGreenDq.TabIndex = 135;
            this.btnGreenDq.UseVisualStyleBackColor = true;
            this.btnGreenDq.Click += new System.EventHandler(this.btnGreenDq_Click);
            // 
            // lblGreenDq
            // 
            this.lblGreenDq.AutoSize = true;
            this.lblGreenDq.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenDq.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblGreenDq.Location = new System.Drawing.Point(6, 70);
            this.lblGreenDq.Name = "lblGreenDq";
            this.lblGreenDq.Size = new System.Drawing.Size(104, 24);
            this.lblGreenDq.TabIndex = 134;
            this.lblGreenDq.Text = "Green DQ";
            this.lblGreenDq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGreenPenalty
            // 
            this.lblGreenPenalty.AutoSize = true;
            this.lblGreenPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenPenalty.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblGreenPenalty.Location = new System.Drawing.Point(7, 35);
            this.lblGreenPenalty.Name = "lblGreenPenalty";
            this.lblGreenPenalty.Size = new System.Drawing.Size(95, 24);
            this.lblGreenPenalty.TabIndex = 132;
            this.lblGreenPenalty.Text = "Penalties";
            this.lblGreenPenalty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGreenFinalScore
            // 
            this.lblGreenFinalScore.AutoSize = true;
            this.lblGreenFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenFinalScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblGreenFinalScore.Location = new System.Drawing.Point(7, 105);
            this.lblGreenFinalScore.Name = "lblGreenFinalScore";
            this.lblGreenFinalScore.Size = new System.Drawing.Size(117, 24);
            this.lblGreenFinalScore.TabIndex = 5;
            this.lblGreenFinalScore.Text = "Final Score";
            this.lblGreenFinalScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnFinalScore
            // 
            this.btnFinalScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFinalScore.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalScore.Location = new System.Drawing.Point(486, 596);
            this.btnFinalScore.Margin = new System.Windows.Forms.Padding(10);
            this.btnFinalScore.Name = "btnFinalScore";
            this.btnFinalScore.Size = new System.Drawing.Size(200, 40);
            this.btnFinalScore.TabIndex = 128;
            this.btnFinalScore.Text = "Final Score";
            this.btnFinalScore.UseVisualStyleBackColor = true;
            this.btnFinalScore.Click += new System.EventHandler(this.btnFinalScore_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbJointScore);
            this.groupBox1.Controls.Add(this.cbEmergencyCycledPenalty);
            this.groupBox1.Controls.Add(this.tbRockWeight);
            this.groupBox1.Controls.Add(this.tbManualEmergencyCycled);
            this.groupBox1.Controls.Add(this.tbManualSolar2Score);
            this.groupBox1.Controls.Add(this.tbManualSolar1Score);
            this.groupBox1.Controls.Add(this.tbAutoSolarScore);
            this.groupBox1.Controls.Add(this.tbAutoEmergencyCycled);
            this.groupBox1.Controls.Add(this.tbAutoCornersTested);
            this.groupBox1.Controls.Add(this.lbRocketLaunchedPointValue);
            this.groupBox1.Controls.Add(this.lbManualEmergencyCycledPointValue);
            this.groupBox1.Controls.Add(this.lbAutoEmergencyCycledPointValue);
            this.groupBox1.Controls.Add(this.lbAutoCornersTestedPointValue);
            this.groupBox1.Controls.Add(this.lbRocketLaunchedScore);
            this.groupBox1.Controls.Add(this.lbManualEmergencyCycledScore);
            this.groupBox1.Controls.Add(this.lbRocketPositionMulitplier);
            this.groupBox1.Controls.Add(this.lbRockScore);
            this.groupBox1.Controls.Add(this.lbAutoEmergencyCycledScore);
            this.groupBox1.Controls.Add(this.lbAutoCornerTestedScore);
            this.groupBox1.Controls.Add(this.cbRocketLaunched);
            this.groupBox1.Controls.Add(this.cbRocketPosition);
            this.groupBox1.Controls.Add(this.lbJointScoreLabel);
            this.groupBox1.Controls.Add(this.lbRocketLaunched);
            this.groupBox1.Controls.Add(this.lbRockWeight);
            this.groupBox1.Controls.Add(this.lbRocketPosition);
            this.groupBox1.Controls.Add(this.lbEmergencyPenalty);
            this.groupBox1.Controls.Add(this.lbManualEmergencyCycled);
            this.groupBox1.Controls.Add(this.lbManualSolar2);
            this.groupBox1.Controls.Add(this.lbManualSolar1);
            this.groupBox1.Controls.Add(this.lbAutoSolar);
            this.groupBox1.Controls.Add(this.lbAutoEmergencyCycled);
            this.groupBox1.Controls.Add(this.lbAutoCornersTested);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Location = new System.Drawing.Point(45, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 424);
            this.groupBox1.TabIndex = 138;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Joint Scores";
            // 
            // cbEmergencyCycledPenalty
            // 
            this.cbEmergencyCycledPenalty.Enabled = false;
            this.cbEmergencyCycledPenalty.ForeColor = System.Drawing.Color.Red;
            this.cbEmergencyCycledPenalty.FormattingEnabled = true;
            this.cbEmergencyCycledPenalty.Items.AddRange(new object[] {
            "0",
            "250"});
            this.cbEmergencyCycledPenalty.Location = new System.Drawing.Point(535, 241);
            this.cbEmergencyCycledPenalty.Name = "cbEmergencyCycledPenalty";
            this.cbEmergencyCycledPenalty.Size = new System.Drawing.Size(100, 33);
            this.cbEmergencyCycledPenalty.TabIndex = 192;
            this.cbEmergencyCycledPenalty.Text = "0";
            // 
            // tbRockWeight
            // 
            this.tbRockWeight.Location = new System.Drawing.Point(320, 311);
            this.tbRockWeight.Name = "tbRockWeight";
            this.tbRockWeight.Size = new System.Drawing.Size(100, 31);
            this.tbRockWeight.TabIndex = 190;
            this.tbRockWeight.Tag = "[0,128]";
            this.tbRockWeight.Text = "0";
            this.tbRockWeight.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            this.tbRockWeight.Validated += new System.EventHandler(this.OnValidation);
            // 
            // tbManualEmergencyCycled
            // 
            this.tbManualEmergencyCycled.Enabled = false;
            this.tbManualEmergencyCycled.Location = new System.Drawing.Point(320, 206);
            this.tbManualEmergencyCycled.Name = "tbManualEmergencyCycled";
            this.tbManualEmergencyCycled.Size = new System.Drawing.Size(100, 31);
            this.tbManualEmergencyCycled.TabIndex = 188;
            this.tbManualEmergencyCycled.Tag = "[0,4]";
            this.tbManualEmergencyCycled.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            // 
            // tbManualSolar2Score
            // 
            this.tbManualSolar2Score.Enabled = false;
            this.tbManualSolar2Score.Location = new System.Drawing.Point(535, 171);
            this.tbManualSolar2Score.Name = "tbManualSolar2Score";
            this.tbManualSolar2Score.Size = new System.Drawing.Size(100, 31);
            this.tbManualSolar2Score.TabIndex = 187;
            this.tbManualSolar2Score.Tag = "[0,480]";
            this.tbManualSolar2Score.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            // 
            // tbManualSolar1Score
            // 
            this.tbManualSolar1Score.Enabled = false;
            this.tbManualSolar1Score.Location = new System.Drawing.Point(535, 136);
            this.tbManualSolar1Score.Name = "tbManualSolar1Score";
            this.tbManualSolar1Score.Size = new System.Drawing.Size(100, 31);
            this.tbManualSolar1Score.TabIndex = 186;
            this.tbManualSolar1Score.Tag = "[0,480]";
            this.tbManualSolar1Score.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            // 
            // tbAutoSolarScore
            // 
            this.tbAutoSolarScore.Enabled = false;
            this.tbAutoSolarScore.Location = new System.Drawing.Point(535, 101);
            this.tbAutoSolarScore.Name = "tbAutoSolarScore";
            this.tbAutoSolarScore.Size = new System.Drawing.Size(100, 31);
            this.tbAutoSolarScore.TabIndex = 185;
            this.tbAutoSolarScore.Tag = "[0,120]";
            this.tbAutoSolarScore.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            // 
            // tbAutoEmergencyCycled
            // 
            this.tbAutoEmergencyCycled.Enabled = false;
            this.tbAutoEmergencyCycled.Location = new System.Drawing.Point(320, 66);
            this.tbAutoEmergencyCycled.Name = "tbAutoEmergencyCycled";
            this.tbAutoEmergencyCycled.Size = new System.Drawing.Size(100, 31);
            this.tbAutoEmergencyCycled.TabIndex = 184;
            this.tbAutoEmergencyCycled.Tag = "[0,4]";
            this.tbAutoEmergencyCycled.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            // 
            // tbAutoCornersTested
            // 
            this.tbAutoCornersTested.Enabled = false;
            this.tbAutoCornersTested.Location = new System.Drawing.Point(320, 31);
            this.tbAutoCornersTested.Name = "tbAutoCornersTested";
            this.tbAutoCornersTested.Size = new System.Drawing.Size(100, 31);
            this.tbAutoCornersTested.TabIndex = 183;
            this.tbAutoCornersTested.Tag = "[0,4]";
            this.tbAutoCornersTested.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            // 
            // lbRocketLaunchedPointValue
            // 
            this.lbRocketLaunchedPointValue.AutoSize = true;
            this.lbRocketLaunchedPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRocketLaunchedPointValue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRocketLaunchedPointValue.Location = new System.Drawing.Point(461, 350);
            this.lbRocketLaunchedPointValue.Name = "lbRocketLaunchedPointValue";
            this.lbRocketLaunchedPointValue.Size = new System.Drawing.Size(43, 24);
            this.lbRocketLaunchedPointValue.TabIndex = 182;
            this.lbRocketLaunchedPointValue.Text = "100";
            this.lbRocketLaunchedPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbManualEmergencyCycledPointValue
            // 
            this.lbManualEmergencyCycledPointValue.AutoSize = true;
            this.lbManualEmergencyCycledPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbManualEmergencyCycledPointValue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbManualEmergencyCycledPointValue.Location = new System.Drawing.Point(461, 210);
            this.lbManualEmergencyCycledPointValue.Name = "lbManualEmergencyCycledPointValue";
            this.lbManualEmergencyCycledPointValue.Size = new System.Drawing.Size(43, 24);
            this.lbManualEmergencyCycledPointValue.TabIndex = 180;
            this.lbManualEmergencyCycledPointValue.Text = "100";
            this.lbManualEmergencyCycledPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAutoEmergencyCycledPointValue
            // 
            this.lbAutoEmergencyCycledPointValue.AutoSize = true;
            this.lbAutoEmergencyCycledPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAutoEmergencyCycledPointValue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbAutoEmergencyCycledPointValue.Location = new System.Drawing.Point(461, 70);
            this.lbAutoEmergencyCycledPointValue.Name = "lbAutoEmergencyCycledPointValue";
            this.lbAutoEmergencyCycledPointValue.Size = new System.Drawing.Size(32, 24);
            this.lbAutoEmergencyCycledPointValue.TabIndex = 179;
            this.lbAutoEmergencyCycledPointValue.Text = "35";
            this.lbAutoEmergencyCycledPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAutoCornersTestedPointValue
            // 
            this.lbAutoCornersTestedPointValue.AutoSize = true;
            this.lbAutoCornersTestedPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAutoCornersTestedPointValue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbAutoCornersTestedPointValue.Location = new System.Drawing.Point(461, 35);
            this.lbAutoCornersTestedPointValue.Name = "lbAutoCornersTestedPointValue";
            this.lbAutoCornersTestedPointValue.Size = new System.Drawing.Size(32, 24);
            this.lbAutoCornersTestedPointValue.TabIndex = 178;
            this.lbAutoCornersTestedPointValue.Text = "15";
            this.lbAutoCornersTestedPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbRocketLaunchedScore
            // 
            this.lbRocketLaunchedScore.AutoSize = true;
            this.lbRocketLaunchedScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRocketLaunchedScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRocketLaunchedScore.Location = new System.Drawing.Point(531, 350);
            this.lbRocketLaunchedScore.Name = "lbRocketLaunchedScore";
            this.lbRocketLaunchedScore.Size = new System.Drawing.Size(21, 24);
            this.lbRocketLaunchedScore.TabIndex = 176;
            this.lbRocketLaunchedScore.Text = "0";
            this.lbRocketLaunchedScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbManualEmergencyCycledScore
            // 
            this.lbManualEmergencyCycledScore.AutoSize = true;
            this.lbManualEmergencyCycledScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbManualEmergencyCycledScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbManualEmergencyCycledScore.Location = new System.Drawing.Point(531, 210);
            this.lbManualEmergencyCycledScore.Name = "lbManualEmergencyCycledScore";
            this.lbManualEmergencyCycledScore.Size = new System.Drawing.Size(21, 24);
            this.lbManualEmergencyCycledScore.TabIndex = 168;
            this.lbManualEmergencyCycledScore.Text = "0";
            this.lbManualEmergencyCycledScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbRocketPositionMulitplier
            // 
            this.lbRocketPositionMulitplier.AutoSize = true;
            this.lbRocketPositionMulitplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRocketPositionMulitplier.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRocketPositionMulitplier.Location = new System.Drawing.Point(465, 315);
            this.lbRocketPositionMulitplier.Name = "lbRocketPositionMulitplier";
            this.lbRocketPositionMulitplier.Size = new System.Drawing.Size(21, 24);
            this.lbRocketPositionMulitplier.TabIndex = 167;
            this.lbRocketPositionMulitplier.Text = "1";
            this.lbRocketPositionMulitplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbRockScore
            // 
            this.lbRockScore.AutoSize = true;
            this.lbRockScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRockScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRockScore.Location = new System.Drawing.Point(531, 315);
            this.lbRockScore.Name = "lbRockScore";
            this.lbRockScore.Size = new System.Drawing.Size(21, 24);
            this.lbRockScore.TabIndex = 166;
            this.lbRockScore.Text = "0";
            this.lbRockScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAutoEmergencyCycledScore
            // 
            this.lbAutoEmergencyCycledScore.AutoSize = true;
            this.lbAutoEmergencyCycledScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAutoEmergencyCycledScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbAutoEmergencyCycledScore.Location = new System.Drawing.Point(531, 70);
            this.lbAutoEmergencyCycledScore.Name = "lbAutoEmergencyCycledScore";
            this.lbAutoEmergencyCycledScore.Size = new System.Drawing.Size(21, 24);
            this.lbAutoEmergencyCycledScore.TabIndex = 165;
            this.lbAutoEmergencyCycledScore.Text = "0";
            this.lbAutoEmergencyCycledScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbAutoCornerTestedScore
            // 
            this.lbAutoCornerTestedScore.AutoSize = true;
            this.lbAutoCornerTestedScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAutoCornerTestedScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbAutoCornerTestedScore.Location = new System.Drawing.Point(531, 35);
            this.lbAutoCornerTestedScore.Name = "lbAutoCornerTestedScore";
            this.lbAutoCornerTestedScore.Size = new System.Drawing.Size(21, 24);
            this.lbAutoCornerTestedScore.TabIndex = 164;
            this.lbAutoCornerTestedScore.Text = "0";
            this.lbAutoCornerTestedScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbRocketLaunched
            // 
            this.cbRocketLaunched.AutoSize = true;
            this.cbRocketLaunched.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbRocketLaunched.Enabled = false;
            this.cbRocketLaunched.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbRocketLaunched.Location = new System.Drawing.Point(405, 355);
            this.cbRocketLaunched.Name = "cbRocketLaunched";
            this.cbRocketLaunched.Size = new System.Drawing.Size(15, 14);
            this.cbRocketLaunched.TabIndex = 160;
            this.cbRocketLaunched.UseVisualStyleBackColor = true;
            this.cbRocketLaunched.CheckedChanged += new System.EventHandler(this.cbRocketLaunched_CheckedChanged);
            // 
            // cbRocketPosition
            // 
            this.cbRocketPosition.FormattingEnabled = true;
            this.cbRocketPosition.Items.AddRange(new object[] {
            "Loaded",
            "Door Closed",
            "Crawler Moved",
            "Launch Position"});
            this.cbRocketPosition.Location = new System.Drawing.Point(223, 276);
            this.cbRocketPosition.Name = "cbRocketPosition";
            this.cbRocketPosition.Size = new System.Drawing.Size(197, 33);
            this.cbRocketPosition.TabIndex = 159;
            this.cbRocketPosition.Text = "Loaded";
            this.cbRocketPosition.TextChanged += new System.EventHandler(this.cbRocketPosition_TextChanged);
            this.cbRocketPosition.Validated += new System.EventHandler(this.OnValidation);
            // 
            // lbJointScoreLabel
            // 
            this.lbJointScoreLabel.AutoSize = true;
            this.lbJointScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbJointScoreLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbJointScoreLabel.Location = new System.Drawing.Point(7, 385);
            this.lbJointScoreLabel.Name = "lbJointScoreLabel";
            this.lbJointScoreLabel.Size = new System.Drawing.Size(115, 24);
            this.lbJointScoreLabel.TabIndex = 158;
            this.lbJointScoreLabel.Text = "Joint Score";
            this.lbJointScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRocketLaunched
            // 
            this.lbRocketLaunched.AutoSize = true;
            this.lbRocketLaunched.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRocketLaunched.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRocketLaunched.Location = new System.Drawing.Point(7, 350);
            this.lbRocketLaunched.Name = "lbRocketLaunched";
            this.lbRocketLaunched.Size = new System.Drawing.Size(173, 24);
            this.lbRocketLaunched.TabIndex = 147;
            this.lbRocketLaunched.Text = "Rocket Launched";
            this.lbRocketLaunched.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRockWeight
            // 
            this.lbRockWeight.AutoSize = true;
            this.lbRockWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRockWeight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRockWeight.Location = new System.Drawing.Point(7, 315);
            this.lbRockWeight.Name = "lbRockWeight";
            this.lbRockWeight.Size = new System.Drawing.Size(189, 24);
            this.lbRockWeight.TabIndex = 146;
            this.lbRockWeight.Text = "Rock Weight/Score";
            this.lbRockWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbRocketPosition
            // 
            this.lbRocketPosition.AutoSize = true;
            this.lbRocketPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRocketPosition.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRocketPosition.Location = new System.Drawing.Point(7, 280);
            this.lbRocketPosition.Name = "lbRocketPosition";
            this.lbRocketPosition.Size = new System.Drawing.Size(154, 24);
            this.lbRocketPosition.TabIndex = 145;
            this.lbRocketPosition.Text = "Rocket Position";
            this.lbRocketPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbEmergencyPenalty
            // 
            this.lbEmergencyPenalty.AutoSize = true;
            this.lbEmergencyPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmergencyPenalty.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbEmergencyPenalty.Location = new System.Drawing.Point(7, 245);
            this.lbEmergencyPenalty.Name = "lbEmergencyPenalty";
            this.lbEmergencyPenalty.Size = new System.Drawing.Size(261, 24);
            this.lbEmergencyPenalty.TabIndex = 144;
            this.lbEmergencyPenalty.Text = "Emergency Cycled Penalty";
            this.lbEmergencyPenalty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbManualEmergencyCycled
            // 
            this.lbManualEmergencyCycled.AutoSize = true;
            this.lbManualEmergencyCycled.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbManualEmergencyCycled.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbManualEmergencyCycled.Location = new System.Drawing.Point(7, 210);
            this.lbManualEmergencyCycled.Name = "lbManualEmergencyCycled";
            this.lbManualEmergencyCycled.Size = new System.Drawing.Size(261, 24);
            this.lbManualEmergencyCycled.TabIndex = 143;
            this.lbManualEmergencyCycled.Text = "Manual Emergency Cycled";
            this.lbManualEmergencyCycled.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbManualSolar2
            // 
            this.lbManualSolar2.AutoSize = true;
            this.lbManualSolar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbManualSolar2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbManualSolar2.Location = new System.Drawing.Point(7, 175);
            this.lbManualSolar2.Name = "lbManualSolar2";
            this.lbManualSolar2.Size = new System.Drawing.Size(208, 24);
            this.lbManualSolar2.TabIndex = 142;
            this.lbManualSolar2.Text = "Manual Solar Panel 2";
            this.lbManualSolar2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbManualSolar1
            // 
            this.lbManualSolar1.AutoSize = true;
            this.lbManualSolar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbManualSolar1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbManualSolar1.Location = new System.Drawing.Point(7, 140);
            this.lbManualSolar1.Name = "lbManualSolar1";
            this.lbManualSolar1.Size = new System.Drawing.Size(208, 24);
            this.lbManualSolar1.TabIndex = 141;
            this.lbManualSolar1.Text = "Manual Solar Panel 1";
            this.lbManualSolar1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAutoSolar
            // 
            this.lbAutoSolar.AutoSize = true;
            this.lbAutoSolar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAutoSolar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbAutoSolar.Location = new System.Drawing.Point(7, 105);
            this.lbAutoSolar.Name = "lbAutoSolar";
            this.lbAutoSolar.Size = new System.Drawing.Size(166, 24);
            this.lbAutoSolar.TabIndex = 140;
            this.lbAutoSolar.Text = "Auto Solar Panel";
            this.lbAutoSolar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAutoEmergencyCycled
            // 
            this.lbAutoEmergencyCycled.AutoSize = true;
            this.lbAutoEmergencyCycled.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAutoEmergencyCycled.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbAutoEmergencyCycled.Location = new System.Drawing.Point(7, 70);
            this.lbAutoEmergencyCycled.Name = "lbAutoEmergencyCycled";
            this.lbAutoEmergencyCycled.Size = new System.Drawing.Size(236, 24);
            this.lbAutoEmergencyCycled.TabIndex = 139;
            this.lbAutoEmergencyCycled.Text = "Auto Emergency Cycled";
            this.lbAutoEmergencyCycled.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbAutoCornersTested
            // 
            this.lbAutoCornersTested.AutoSize = true;
            this.lbAutoCornersTested.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAutoCornersTested.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbAutoCornersTested.Location = new System.Drawing.Point(7, 35);
            this.lbAutoCornersTested.Name = "lbAutoCornersTested";
            this.lbAutoCornersTested.Size = new System.Drawing.Size(203, 24);
            this.lbAutoCornersTested.TabIndex = 138;
            this.lbAutoCornersTested.Text = "Auto Corners Tested";
            this.lbAutoCornersTested.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbJointScore
            // 
            this.lbJointScore.AutoSize = true;
            this.lbJointScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbJointScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbJointScore.Location = new System.Drawing.Point(531, 385);
            this.lbJointScore.Name = "lbJointScore";
            this.lbJointScore.Size = new System.Drawing.Size(21, 24);
            this.lbJointScore.TabIndex = 193;
            this.lbJointScore.Text = "0";
            this.lbJointScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbGreenScore
            // 
            this.lbGreenScore.AutoSize = true;
            this.lbGreenScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreenScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbGreenScore.Location = new System.Drawing.Point(239, 105);
            this.lbGreenScore.Name = "lbGreenScore";
            this.lbGreenScore.Size = new System.Drawing.Size(21, 24);
            this.lbGreenScore.TabIndex = 194;
            this.lbGreenScore.Text = "0";
            this.lbGreenScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbRedScore
            // 
            this.lbRedScore.AutoSize = true;
            this.lbRedScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRedScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRedScore.Location = new System.Drawing.Point(239, 105);
            this.lbRedScore.Name = "lbRedScore";
            this.lbRedScore.Size = new System.Drawing.Size(21, 24);
            this.lbRedScore.TabIndex = 195;
            this.lbRedScore.Text = "0";
            this.lbRedScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Score
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 651);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOverride);
            this.Controls.Add(this.btnUpdateScore);
            this.Controls.Add(this.grRedFinalScore);
            this.Controls.Add(this.gbGreenFinalScore);
            this.Controls.Add(this.btnFinalScore);
            this.Name = "Score";
            this.Text = "Score";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Score_FormClosed);
            this.Load += new System.EventHandler(this.Score_Shown);
            this.grRedFinalScore.ResumeLayout(false);
            this.grRedFinalScore.PerformLayout();
            this.gbGreenFinalScore.ResumeLayout(false);
            this.gbGreenFinalScore.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnOverride;
        public System.Windows.Forms.Button btnUpdateScore;
        private System.Windows.Forms.GroupBox grRedFinalScore;
        private System.Windows.Forms.Button btnRedDq;
        private System.Windows.Forms.Label lblRedDQ;
        private System.Windows.Forms.Label lblRedPenalty;
        private System.Windows.Forms.Label lblRedFinalScore;
        private System.Windows.Forms.GroupBox gbGreenFinalScore;
        private System.Windows.Forms.Button btnGreenDq;
        private System.Windows.Forms.Label lblGreenDq;
        private System.Windows.Forms.Label lblGreenPenalty;
        private System.Windows.Forms.Label lblGreenFinalScore;
        public System.Windows.Forms.Button btnFinalScore;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbManualSolar2;
        private System.Windows.Forms.Label lbManualSolar1;
        private System.Windows.Forms.Label lbAutoSolar;
        private System.Windows.Forms.Label lbAutoEmergencyCycled;
        private System.Windows.Forms.Label lbAutoCornersTested;
        private System.Windows.Forms.Label lbRocketPosition;
        private System.Windows.Forms.Label lbEmergencyPenalty;
        private System.Windows.Forms.Label lbManualEmergencyCycled;
        private System.Windows.Forms.Label lbRockWeight;
        private System.Windows.Forms.Label lbRocketLaunched;
        private System.Windows.Forms.Label lbJointScoreLabel;
        private System.Windows.Forms.CheckBox cbRocketLaunched;
        private System.Windows.Forms.ComboBox cbRocketPosition;
        private System.Windows.Forms.Label lbAutoEmergencyCycledScore;
        private System.Windows.Forms.Label lbAutoCornerTestedScore;
        private System.Windows.Forms.Label lbRockScore;
        private System.Windows.Forms.Label lbRedPenaltyScore;
        private System.Windows.Forms.Label lbGreenPenaltyScore;
        private System.Windows.Forms.Label lbRocketLaunchedScore;
        private System.Windows.Forms.Label lbManualEmergencyCycledScore;
        private System.Windows.Forms.Label lbRocketPositionMulitplier;
        private System.Windows.Forms.Label lbManualEmergencyCycledPointValue;
        private System.Windows.Forms.Label lbAutoEmergencyCycledPointValue;
        private System.Windows.Forms.Label lbAutoCornersTestedPointValue;
        private System.Windows.Forms.TextBox tbRedPenalty;
        private System.Windows.Forms.TextBox tbGreenPenalty;
        private System.Windows.Forms.TextBox tbRockWeight;
        private System.Windows.Forms.TextBox tbManualEmergencyCycled;
        private System.Windows.Forms.TextBox tbManualSolar2Score;
        private System.Windows.Forms.TextBox tbManualSolar1Score;
        private System.Windows.Forms.TextBox tbAutoSolarScore;
        private System.Windows.Forms.TextBox tbAutoEmergencyCycled;
        private System.Windows.Forms.TextBox tbAutoCornersTested;
        private System.Windows.Forms.ComboBox cbEmergencyCycledPenalty;
        private System.Windows.Forms.Label lbRedPenaltyPointValue;
        private System.Windows.Forms.Label lbGreenPenaltyPointValue;
        private System.Windows.Forms.Label lbRocketLaunchedPointValue;
        private System.Windows.Forms.Label lbJointScore;
        private System.Windows.Forms.Label lbRedScore;
        private System.Windows.Forms.Label lbGreenScore;
    }
}