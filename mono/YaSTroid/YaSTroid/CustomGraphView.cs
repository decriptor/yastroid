using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;

namespace YaSTroid
{
	public class CustomGraphView : View 
	{
		public static bool BAR = true;
		public static bool LINE = false;

		Paint paint;
		float[] values;
		string[] horlabels;
		string[] verlabels;
		string title;
		bool type;
		bool parmsSet=false;

		public CustomGraphView (Context context, IAttributeSet attrs) :	base (context, attrs)
		{
			Initialize ();
		}
		
		public CustomGraphView (Context context, IAttributeSet attrs, int defStyle) : base (context, attrs, defStyle)
		{
			Initialize ();
		}
		
		void Initialize ()
		{
		}

		public void setCustomGraphViewParms (float[] values, string title, string[] horlabels, string[] verlabels, bool type)
		{
			if (values == null)
				values = new float[0];
			else
				this.values = values;
			if (title == null)
				title = "";
			else
				this.title = title;
			if (horlabels == null)
				this.horlabels = new string[0];
			else
				this.horlabels = horlabels;
			if (verlabels == null)
				this.verlabels = new string[0];
			else
				this.verlabels = verlabels;
			this.type = type;
			paint = new Paint ();
			parmsSet = true;
			Invalidate();
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);

			if (!parmsSet)
				return;
			float border = 20;
			float horstart = border * 2;
			float height = Height;
			float width = Width - 1;
			float max = getMax();
			float min = getMin();
			float diff = max - min;
			float graphheight = height - (2 * border);
			float graphwidth = width - (2 * border);

			paint.TextAlign = Paint.Align.Left;
			int vers = verlabels.Length - 1;

			for (int i = 0; i < verlabels.Length; i++)
			{
				paint.Color = Color.DarkGray;
				float y = ((graphheight / vers) * i) + border;
				canvas.DrawLine(horstart, y, width, y, paint);
				paint.Color = Color.White;
				canvas.DrawText(verlabels[i], 0, y, paint);
			}
			int hors = horlabels.Length - 1;
			for (int i = 0; i < horlabels.Length; i++) {
				paint.Color = Color.DarkGray;
				float x = ((graphwidth / hors) * i) + horstart;
				canvas.DrawLine(x, height - border, x, border, paint);
				paint.TextAlign = Paint.Align.Center;
				if (i==horlabels.Length-1)
					paint.TextAlign = Paint.Align.Right;
				if (i==0)
					paint.TextAlign = Paint.Align.Left;
				paint.Color = Color.White;
				canvas.DrawText(horlabels[i], x, height - 4, paint);
			}

			paint.TextAlign = Paint.Align.Center;
			canvas.DrawText(title, (graphwidth / 2) + horstart, border - 4, paint);

			if (max != min) {
				paint.Color = Color.LightGray;
				if (type == BAR) {
					float datalength = values.Length;
					float colwidth = (width - (2 * border)) / datalength;
					for (int i = 0; i < values.Length; i++) {
						float val = values[i] - min;
						float rat = val / diff;
						float h = graphheight * rat;
						canvas.DrawRect((i * colwidth) + horstart, (border - h) + graphheight, ((i * colwidth) + horstart) + (colwidth - 1), height - (border - 1), paint);
					}
				} else {
					float datalength = values.Length;
					float colwidth = (width - (2 * border)) / datalength;
					float halfcol = colwidth / 2;
					float lasth = 0;
					for (int i = 0; i < values.Length; i++) {
						float val = values[i] - min;
						float rat = val / diff;
						float h = graphheight * rat;
						if (i > 0)
							canvas.DrawLine(((i - 1) * colwidth) + (horstart + 1) + halfcol, (border - lasth) + graphheight, (i * colwidth) + (horstart + 1) + halfcol, (border - h) + graphheight, paint);
						lasth = h;
					}
				}
			}
		}

		float getMax()
		{
			float largest = int.MinValue;
			for (int i = 0; i < values.Length; i++)
				if (values[i] > largest)
					largest = values[i];
			return largest;
		}

		float getMin()
		{
			float smallest = int.MaxValue;
			for (int i = 0; i < values.Length; i++)
				if (values[i] < smallest)
					smallest = values[i];
			return smallest;
		}
	}
}
