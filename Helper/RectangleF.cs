//------------------------------------------------------------------------------ 
// <copyright file="RectangleF.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 

using System;

namespace CUE.NET
{
	public struct RectangleF { 
		public static readonly RectangleF Empty = new RectangleF(); 

        private float x; 
        private float y;
        private float width;
        private float height;

        public RectangleF(float x, float y, float width, float height) {
            this.x = x; 
            this.y = y;
            this.width = width; 
            this.height = height; 
        }
 
        public float X {
            get {
                return x;
            } 
            set {
                x = value; 
            } 
        }
 
        public float Y {
            get { 
                return y;
            }
            set {
                y = value; 
            }
        } 
 
       public float Width { 
            get { 
                return width;
            } 
            set {
                width = value;
            }
        } 

       public float Height { 
            get {
                return height; 
            } 
            set {
                height = value; 
            }
        }

		public PointF Location {
			get
			{
				return new PointF(x,y);
			}
			set
			{
				x = value.X;
				y = value.Y;
			}
		}

		public float Left
		{
			get
			{
				return X;
			}
		}

		public float Right
		{
			get
			{
				return X + Width;
			}
		}

		public float Top
		{
			get
			{
				return Y;
			}
		}

		public float Bottom
		{
			get
			{
				return Y + Height;
			}
		}


		public bool Contains(float x, float y)
		{
			return ((x >= Left) && (x < Right) &&
				(y >= Top) && (y < Bottom));
		}

		public bool Contains(PointF pt)
		{
			return Contains(pt.X, pt.Y);
		}

		public bool Contains(RectangleF rect)
		{
			return X <= rect.X && Right >= rect.Right && Y <= rect.Y && Bottom >= rect.Bottom;
		}

		public static bool operator ==(RectangleF left, RectangleF right)
		{
			return (left.X == right.X) && (left.Y == right.Y) &&
								(left.Width == right.Width) && (left.Height == right.Height);
		}

		public static bool operator !=(RectangleF left, RectangleF right)
		{
			return (left.X != right.X) || (left.Y != right.Y) ||
								(left.Width != right.Width) || (left.Height != right.Height);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is RectangleF))
				return false;

			return (this == (RectangleF)obj);
		}

		public override int GetHashCode()
		{
			return (int)(x + y + width + height);
		}

		public bool IntersectsWith(RectangleF rect)
		{
			return !((Left >= rect.Right) || (Right <= rect.Left) ||
				(Top >= rect.Bottom) || (Bottom <= rect.Top));
		}

		private bool IntersectsWithInclusive(RectangleF r)
		{
			return !((Left > r.Right) || (Right < r.Left) ||
				(Top > r.Bottom) || (Bottom < r.Top));
		}

		public static RectangleF FromLTRB(float left, float top,
				   float right, float bottom)
		{
			return new RectangleF(left, top, right - left, bottom - top);
		}

		public static RectangleF Intersect(RectangleF a,
					RectangleF b)
		{
			// MS.NET returns a non-empty rectangle if the two rectangles
			// touch each other
			if (!a.IntersectsWithInclusive(b))
				return Empty;

			return FromLTRB(
				Math.Max(a.Left, b.Left),
				Math.Max(a.Top, b.Top),
				Math.Min(a.Right, b.Right),
				Math.Min(a.Bottom, b.Bottom));
		}

		public void Intersect(RectangleF rect)
		{
			this = RectangleF.Intersect(this, rect);
		}

		public bool IsEmpty
		{
			get
			{
				return (width <= 0 || height <= 0);
			}
		}
	}
}