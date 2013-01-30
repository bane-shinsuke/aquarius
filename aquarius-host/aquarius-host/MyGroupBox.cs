using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace aquarius_host
{
    partial class MyGroupBox : GroupBox
    {
        // 枠色（デフォルト：白）
        private Color borderColor = Color.White;

        // コンストラクタ
        public MyGroupBox()
        {
            // ダブルバッファリングを有効
            SetStyle(ControlStyles.DoubleBuffer, true);

            // グループボックスの描画をオーナードローにする
            SetStyle(ControlStyles.UserPaint, true);
        }

        // プロパティ
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
            }
        }

        // OnPrintイベント
        protected override void OnPaint(PaintEventArgs e)
        {
            // テキストサイズを取得
            Size tTextSize = TextRenderer.MeasureText(this.Text, this.Font);

            // グループボックスの領域を取得
            Rectangle tBorderRect = e.ClipRectangle;

            // テキストを考慮（グループボックス枠線がテキスト（高さ）の真ん中に来るように）して枠を描画
            tBorderRect.Y += tTextSize.Height / 2;
            tBorderRect.Height -= tTextSize.Height / 2;
            ControlPaint.DrawBorder(e.Graphics, tBorderRect, borderColor, ButtonBorderStyle.Solid);

            // テキストを描画
            Rectangle tTextRect = e.ClipRectangle;
            tTextRect.X += 6;                           // テキストの描画開始位置(X)をグループボックスの領域から6ドットずらす
            tTextRect.Width = tTextSize.Width;
            tTextRect.Height = tTextSize.Height;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), tTextRect);
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), tTextRect);
        }
    }
}
