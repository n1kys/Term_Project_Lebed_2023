﻿namespace Term_Project_Lebed_2023
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label2 = new Label();
            dataGridView2 = new DataGridView();
            groupBox1 = new GroupBox();
            acceptButton = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            renameFileRadio = new RadioButton();
            deleteFileRadio = new RadioButton();
            uploadFileRadio = new RadioButton();
            createFileRadio = new RadioButton();
            tabControl2 = new TabControl();
            tabPage3 = new TabPage();
            button7 = new Button();
            radioButton14 = new RadioButton();
            radioButton13 = new RadioButton();
            radioButton12 = new RadioButton();
            radioButton11 = new RadioButton();
            radioButton10 = new RadioButton();
            tabPage4 = new TabPage();
            button6 = new Button();
            radioButton9 = new RadioButton();
            radioButton8 = new RadioButton();
            radioButton7 = new RadioButton();
            radioButton6 = new RadioButton();
            radioButton5 = new RadioButton();
            tabPage2 = new TabPage();
            addRow_Button = new Button();
            deleteRow_Button = new Button();
            cancelEdit_Button = new Button();
            save_Button = new Button();
            dataGridView1 = new DataGridView();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            groupBox1.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(900, 500);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(dataGridView2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(tabControl2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(892, 472);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Main page";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 235);
            label2.Name = "label2";
            label2.Size = new Size(175, 15);
            label2.TabIndex = 2;
            label2.Text = "Info about search by parameter:";
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(8, 257);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(874, 207);
            dataGridView2.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(acceptButton);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(renameFileRadio);
            groupBox1.Controls.Add(deleteFileRadio);
            groupBox1.Controls.Add(uploadFileRadio);
            groupBox1.Controls.Add(createFileRadio);
            groupBox1.Location = new Point(8, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(374, 207);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Actions with DataBase";
            // 
            // acceptButton
            // 
            acceptButton.Enabled = false;
            acceptButton.FlatStyle = FlatStyle.Flat;
            acceptButton.Location = new Point(197, 163);
            acceptButton.Name = "acceptButton";
            acceptButton.Size = new Size(166, 23);
            acceptButton.TabIndex = 2;
            acceptButton.Text = "Accept";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += acceptButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 140);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 6;
            label1.Text = "Info text";
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(6, 163);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(175, 23);
            textBox1.TabIndex = 5;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // renameFileRadio
            // 
            renameFileRadio.AutoSize = true;
            renameFileRadio.Location = new Point(6, 107);
            renameFileRadio.Name = "renameFileRadio";
            renameFileRadio.Size = new Size(138, 19);
            renameFileRadio.TabIndex = 4;
            renameFileRadio.TabStop = true;
            renameFileRadio.Text = "Rename file DataBase";
            renameFileRadio.UseVisualStyleBackColor = true;
            renameFileRadio.CheckedChanged += radioButton_CheckedChanged;
            // 
            // deleteFileRadio
            // 
            deleteFileRadio.AutoSize = true;
            deleteFileRadio.Location = new Point(6, 82);
            deleteFileRadio.Name = "deleteFileRadio";
            deleteFileRadio.Size = new Size(128, 19);
            deleteFileRadio.TabIndex = 3;
            deleteFileRadio.TabStop = true;
            deleteFileRadio.Text = "Delete file DataBase";
            deleteFileRadio.UseVisualStyleBackColor = true;
            deleteFileRadio.CheckedChanged += radioButton_CheckedChanged;
            // 
            // uploadFileRadio
            // 
            uploadFileRadio.AutoSize = true;
            uploadFileRadio.Location = new Point(6, 57);
            uploadFileRadio.Name = "uploadFileRadio";
            uploadFileRadio.Size = new Size(133, 19);
            uploadFileRadio.TabIndex = 2;
            uploadFileRadio.TabStop = true;
            uploadFileRadio.Text = "Upload file DataBase";
            uploadFileRadio.UseVisualStyleBackColor = true;
            uploadFileRadio.CheckedChanged += radioButton_CheckedChanged;
            // 
            // createFileRadio
            // 
            createFileRadio.AutoSize = true;
            createFileRadio.Location = new Point(6, 32);
            createFileRadio.Name = "createFileRadio";
            createFileRadio.Size = new Size(163, 19);
            createFileRadio.TabIndex = 1;
            createFileRadio.TabStop = true;
            createFileRadio.Text = "Create a new file DataBase";
            createFileRadio.UseVisualStyleBackColor = true;
            createFileRadio.CheckedChanged += radioButton_CheckedChanged;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage3);
            tabControl2.Controls.Add(tabPage4);
            tabControl2.Location = new Point(411, 6);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(475, 220);
            tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(button7);
            tabPage3.Controls.Add(radioButton14);
            tabPage3.Controls.Add(radioButton13);
            tabPage3.Controls.Add(radioButton12);
            tabPage3.Controls.Add(radioButton11);
            tabPage3.Controls.Add(radioButton10);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(467, 192);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "Search page";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.FlatStyle = FlatStyle.Flat;
            button7.Location = new Point(6, 151);
            button7.Name = "button7";
            button7.Size = new Size(455, 35);
            button7.TabIndex = 5;
            button7.Text = "Search";
            button7.UseVisualStyleBackColor = true;
            // 
            // radioButton14
            // 
            radioButton14.AutoSize = true;
            radioButton14.Location = new Point(18, 121);
            radioButton14.Name = "radioButton14";
            radioButton14.Size = new Size(150, 19);
            radioButton14.TabIndex = 4;
            radioButton14.TabStop = true;
            radioButton14.Text = "Search by info about ad";
            radioButton14.UseVisualStyleBackColor = true;
            // 
            // radioButton13
            // 
            radioButton13.AutoSize = true;
            radioButton13.Location = new Point(18, 96);
            radioButton13.Name = "radioButton13";
            radioButton13.Size = new Size(143, 19);
            radioButton13.TabIndex = 3;
            radioButton13.TabStop = true;
            radioButton13.Text = "Search by permit, date";
            radioButton13.UseVisualStyleBackColor = true;
            // 
            // radioButton12
            // 
            radioButton12.AutoSize = true;
            radioButton12.Location = new Point(18, 71);
            radioButton12.Name = "radioButton12";
            radioButton12.Size = new Size(126, 19);
            radioButton12.TabIndex = 2;
            radioButton12.TabStop = true;
            radioButton12.Text = "Search by ad's type";
            radioButton12.UseVisualStyleBackColor = true;
            // 
            // radioButton11
            // 
            radioButton11.AutoSize = true;
            radioButton11.Location = new Point(18, 46);
            radioButton11.Name = "radioButton11";
            radioButton11.Size = new Size(205, 19);
            radioButton11.TabIndex = 1;
            radioButton11.TabStop = true;
            radioButton11.Text = "Search by contract's number, date";
            radioButton11.UseVisualStyleBackColor = true;
            // 
            // radioButton10
            // 
            radioButton10.AutoSize = true;
            radioButton10.Location = new Point(18, 21);
            radioButton10.Name = "radioButton10";
            radioButton10.Size = new Size(164, 19);
            radioButton10.TabIndex = 0;
            radioButton10.TabStop = true;
            radioButton10.Text = "Search by name of subject";
            radioButton10.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(button6);
            tabPage4.Controls.Add(radioButton9);
            tabPage4.Controls.Add(radioButton8);
            tabPage4.Controls.Add(radioButton7);
            tabPage4.Controls.Add(radioButton6);
            tabPage4.Controls.Add(radioButton5);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(467, 192);
            tabPage4.TabIndex = 1;
            tabPage4.Text = "Sort page";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.FlatStyle = FlatStyle.Flat;
            button6.Location = new Point(6, 151);
            button6.Name = "button6";
            button6.Size = new Size(455, 35);
            button6.TabIndex = 5;
            button6.Text = "Sort";
            button6.UseVisualStyleBackColor = true;
            // 
            // radioButton9
            // 
            radioButton9.AutoSize = true;
            radioButton9.Location = new Point(18, 121);
            radioButton9.Name = "radioButton9";
            radioButton9.Size = new Size(136, 19);
            radioButton9.TabIndex = 4;
            radioButton9.Text = "Sort by info about ad";
            radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            radioButton8.AutoSize = true;
            radioButton8.Location = new Point(18, 96);
            radioButton8.Name = "radioButton8";
            radioButton8.Size = new Size(129, 19);
            radioButton8.TabIndex = 3;
            radioButton8.Text = "Sort by permit, date";
            radioButton8.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(18, 71);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(112, 19);
            radioButton7.TabIndex = 2;
            radioButton7.Text = "Sort by ad's type";
            radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(18, 46);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(191, 19);
            radioButton6.TabIndex = 1;
            radioButton6.Text = "Sort by contract's number, date";
            radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Checked = true;
            radioButton5.Location = new Point(18, 21);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(150, 19);
            radioButton5.TabIndex = 0;
            radioButton5.TabStop = true;
            radioButton5.Text = "Sort by name of subject";
            radioButton5.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(addRow_Button);
            tabPage2.Controls.Add(deleteRow_Button);
            tabPage2.Controls.Add(cancelEdit_Button);
            tabPage2.Controls.Add(save_Button);
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(892, 472);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Table page";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // addRow_Button
            // 
            addRow_Button.FlatStyle = FlatStyle.Flat;
            addRow_Button.Location = new Point(240, 399);
            addRow_Button.Name = "addRow_Button";
            addRow_Button.Size = new Size(230, 55);
            addRow_Button.TabIndex = 4;
            addRow_Button.Text = "add row";
            addRow_Button.UseVisualStyleBackColor = true;
            addRow_Button.Click += addRow_Button_Click;
            // 
            // deleteRow_Button
            // 
            deleteRow_Button.FlatStyle = FlatStyle.Flat;
            deleteRow_Button.Location = new Point(476, 399);
            deleteRow_Button.Name = "deleteRow_Button";
            deleteRow_Button.Size = new Size(203, 55);
            deleteRow_Button.TabIndex = 3;
            deleteRow_Button.Text = "delete row";
            deleteRow_Button.UseVisualStyleBackColor = true;
            deleteRow_Button.Click += deleteRow_Button_Click;
            // 
            // cancelEdit_Button
            // 
            cancelEdit_Button.FlatStyle = FlatStyle.Flat;
            cancelEdit_Button.Location = new Point(685, 399);
            cancelEdit_Button.Name = "cancelEdit_Button";
            cancelEdit_Button.Size = new Size(201, 55);
            cancelEdit_Button.TabIndex = 2;
            cancelEdit_Button.Text = "Cancel button";
            cancelEdit_Button.UseVisualStyleBackColor = true;
            cancelEdit_Button.Click += cancelEdit_Button_Click;
            // 
            // save_Button
            // 
            save_Button.FlatStyle = FlatStyle.Flat;
            save_Button.Location = new Point(8, 399);
            save_Button.Name = "save_Button";
            save_Button.Size = new Size(226, 55);
            save_Button.TabIndex = 1;
            save_Button.Text = "Save button";
            save_Button.UseVisualStyleBackColor = true;
            save_Button.Click += save_Button_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(8, 8);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(878, 375);
            dataGridView1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 500);
            Controls.Add(tabControl1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DataBase Assistance";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabControl2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridView1;
        private Button addRow_Button;
        private Button deleteRow_Button;
        private Button cancelEdit_Button;
        private Button save_Button;
        private TabControl tabControl2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private GroupBox groupBox1;
        private Button acceptButton;
        private Label label1;
        private TextBox textBox1;
        private RadioButton renameFileRadio;
        private RadioButton deleteFileRadio;
        private RadioButton uploadFileRadio;
        private RadioButton createFileRadio;
        private DataGridView dataGridView2;
        private Label label2;
        private Button button6;
        private RadioButton radioButton9;
        private RadioButton radioButton8;
        private RadioButton radioButton7;
        private RadioButton radioButton6;
        private RadioButton radioButton5;
        private Button button7;
        private RadioButton radioButton14;
        private RadioButton radioButton13;
        private RadioButton radioButton12;
        private RadioButton radioButton11;
        private RadioButton radioButton10;
    }
}