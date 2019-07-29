namespace Stocks.Windows
{
    partial class addStockForm
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
            this.setID = new System.Windows.Forms.TextBox();
            this.setStockName = new System.Windows.Forms.TextBox();
            this.setStockVolume = new System.Windows.Forms.TextBox();
            this.setStockPrice = new System.Windows.Forms.TextBox();
            this.stockID = new System.Windows.Forms.Label();
            this.stockName = new System.Windows.Forms.Label();
            this.stockVolume = new System.Windows.Forms.Label();
            this.stockPrice = new System.Windows.Forms.Label();
            this.saveStock = new System.Windows.Forms.Button();
            this.backToForm1 = new System.Windows.Forms.Button();
            this.errorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // setID
            // 
            this.setID.Location = new System.Drawing.Point(36, 53);
            this.setID.Name = "setID";
            this.setID.Size = new System.Drawing.Size(100, 20);
            this.setID.TabIndex = 0;
            // 
            // setStockName
            // 
            this.setStockName.Location = new System.Drawing.Point(142, 53);
            this.setStockName.Name = "setStockName";
            this.setStockName.Size = new System.Drawing.Size(100, 20);
            this.setStockName.TabIndex = 1;
            // 
            // setStockVolume
            // 
            this.setStockVolume.Location = new System.Drawing.Point(248, 52);
            this.setStockVolume.Name = "setStockVolume";
            this.setStockVolume.Size = new System.Drawing.Size(100, 20);
            this.setStockVolume.TabIndex = 2;
            // 
            // setStockPrice
            // 
            this.setStockPrice.Location = new System.Drawing.Point(354, 53);
            this.setStockPrice.Name = "setStockPrice";
            this.setStockPrice.Size = new System.Drawing.Size(100, 20);
            this.setStockPrice.TabIndex = 3;
            // 
            // stockID
            // 
            this.stockID.AutoSize = true;
            this.stockID.Location = new System.Drawing.Point(36, 34);
            this.stockID.Name = "stockID";
            this.stockID.Size = new System.Drawing.Size(18, 13);
            this.stockID.TabIndex = 4;
            this.stockID.Text = "ID";
            // 
            // stockName
            // 
            this.stockName.AutoSize = true;
            this.stockName.Location = new System.Drawing.Point(142, 34);
            this.stockName.Name = "stockName";
            this.stockName.Size = new System.Drawing.Size(35, 13);
            this.stockName.TabIndex = 5;
            this.stockName.Text = "Name";
            // 
            // stockVolume
            // 
            this.stockVolume.AutoSize = true;
            this.stockVolume.Location = new System.Drawing.Point(248, 34);
            this.stockVolume.Name = "stockVolume";
            this.stockVolume.Size = new System.Drawing.Size(42, 13);
            this.stockVolume.TabIndex = 6;
            this.stockVolume.Text = "Volume";
            // 
            // stockPrice
            // 
            this.stockPrice.AutoSize = true;
            this.stockPrice.Location = new System.Drawing.Point(354, 34);
            this.stockPrice.Name = "stockPrice";
            this.stockPrice.Size = new System.Drawing.Size(31, 13);
            this.stockPrice.TabIndex = 7;
            this.stockPrice.Text = "Price";
            // 
            // saveStock
            // 
            this.saveStock.Location = new System.Drawing.Point(145, 94);
            this.saveStock.Name = "saveStock";
            this.saveStock.Size = new System.Drawing.Size(203, 38);
            this.saveStock.TabIndex = 8;
            this.saveStock.Text = "Save";
            this.saveStock.UseVisualStyleBackColor = true;
            this.saveStock.Click += new System.EventHandler(this.SaveStock_Click);
            // 
            // backToForm1
            // 
            this.backToForm1.Location = new System.Drawing.Point(575, 13);
            this.backToForm1.Name = "backToForm1";
            this.backToForm1.Size = new System.Drawing.Size(93, 34);
            this.backToForm1.TabIndex = 9;
            this.backToForm1.Text = "Back";
            this.backToForm1.UseVisualStyleBackColor = true;
            this.backToForm1.Click += new System.EventHandler(this.BackToForm1_Click);
            // 
            // errorMessage
            // 
            this.errorMessage.AutoSize = true;
            this.errorMessage.Location = new System.Drawing.Point(39, 165);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(0, 13);
            this.errorMessage.TabIndex = 10;
            // 
            // addStockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.backToForm1);
            this.Controls.Add(this.saveStock);
            this.Controls.Add(this.stockPrice);
            this.Controls.Add(this.stockVolume);
            this.Controls.Add(this.stockName);
            this.Controls.Add(this.stockID);
            this.Controls.Add(this.setStockPrice);
            this.Controls.Add(this.setStockVolume);
            this.Controls.Add(this.setStockName);
            this.Controls.Add(this.setID);
            this.Name = "addStockForm";
            this.Text = "AddStock";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox setID;
        private System.Windows.Forms.TextBox setStockName;
        private System.Windows.Forms.TextBox setStockVolume;
        private System.Windows.Forms.TextBox setStockPrice;
        private System.Windows.Forms.Label stockID;
        private System.Windows.Forms.Label stockName;
        private System.Windows.Forms.Label stockVolume;
        private System.Windows.Forms.Label stockPrice;
        private System.Windows.Forms.Button saveStock;
        private System.Windows.Forms.Button backToForm1;
        private System.Windows.Forms.Label errorMessage;
    }
}