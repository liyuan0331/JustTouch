using System;
using UnityEngine;

class ColorHSV {
	public float H;
	public float S;
	public float V;

	public ColorHSV() {
		H = 0;
		S = 0;
		V = 0;
	}

	public ColorHSV(float h, float s, float v) {
		H = Math.Max(0f, Math.Min(1f, h));
		S = Math.Max(0f, Math.Min(1f, s));
		V = Math.Max(0f, Math.Min(1f, v));
	}
}

class ColorConverter {
	// RGB 转换为 HSV
	public static ColorHSV RGB2HSV(Color color) {
		float r = color.r;
		float g = color.g;
		float b = color.b;

		float max = Math.Max(Math.Max(r, g), b);
		float min = Math.Min(Math.Min(r, g), b);

		float h = max - min;

		if (h > 0f) {
			if (max == r) {
				h = (g - b) / h;
				if (h < 0f) {
					h += 6f;
				}
			} else if (max == g) {
				h = 2f + (b - r) / h;
			} else {
				h = 4f + (r - g) / h;
			}
		}
		h /= 6f;

		float s = max - min;
		if (max != 0) {
			s /= max;
		}
		float v = max;

		return new ColorHSV(h, s, v);
	}

	// HSV 转换为 RGB
	public static Color HSV2RGB(ColorHSV hsv) {

		if (hsv == null) {
			return new Color();
		}

		float h = hsv.H;
		float s = hsv.S;
		float v = hsv.V;

		float r = v;
		float g = v;
		float b = v;

		if (s > 0f) {
			h *= 6f;
			int i = (int)h;
			float f = h - (float)i;
			switch (i) {
			default:
			case 0:
				g *= 1f - s * (1f - f);
				b *= 1f - s;
				break;
			case 1:
				r *= 1f - s * f;
				b *= 1f - s;
				break;
			case 2:
				r *= 1f - s;
				b *= 1f - s * (1f - f);
				break;
			case 3:
				r *= 1f - s;
				g *= 1f - s * f;
				break;
			case 4:
				r *= 1f - s * (1f - f);
				g *= 1f - s;
				break;
			case 5:
				g *= 1f - s;
				b *= 1f - s * f;
				break;
			}
		}

//		r *= 255f;
//		g *= 255f;
//		b *= 255f;

		Color result = new Color ();
		result = new Color(r, g, b);
		return result;
	}
}