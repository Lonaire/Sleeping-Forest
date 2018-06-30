using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingForest {
	public class BigInt {
		public float value { get; set; }
		public int exp { get; set; }

		public BigInt() {
			value = 0;
			exp = 0;
		}

		public BigInt(float val) {
			exp = 0;
			while (val >= 10) {
				val /= 10;
				exp++;
			}
			if (val == 0 && exp > 0) {
				val = 0.1f;
				exp--;
			}
			value = val;
		}

		public BigInt(float val, int e) {
			exp = e;
			while (val >= 10) {
				val /= 10;
				exp++;
			}
			if (val == 0 && exp > 0) {
				val = 0.1f;
				exp--;
			}
			value = val;
		}

		//Сложение (BigInt, float)
		public static BigInt operator +(BigInt left, float right) {
			BigInt result = new BigInt (left.value, left.exp);
			result.value += right;
			while (result.value >= 10) {
				result.value /= 10;
				result.exp++;
			}
			return result;
		}

		//Вычитание (BigInt, float)
		public static BigInt operator -(BigInt left, float right) {
			BigInt result = new BigInt (left.value, left.exp);
			result.value -= right;
			while (result.value >= 10) {
				result.value /= 10;
				result.exp++;
			}
			while (result.value < 1 && result.exp > 0) {
				result.value *= 10;
				result.exp--;
			}
			return result;
		}

		//Сложение (BigInt, BigInt)
		public static BigInt operator +(BigInt left, BigInt right) {
			BigInt result = new BigInt (left.value, left.exp);
			BigInt _right = new BigInt (right.value, right.exp);
			while (result.exp > _right.exp) {
				_right.value /= 10;
				_right.exp++;
			}
			while (result.exp < _right.exp) {
				result.value /= 10;
				result.exp++;
			}
			result.value += _right.value;
			while (result.value >= 10) {
				result.value /= 10;
				result.exp++;
			}
			while (result.value < 1) {
				result.value *= 10;
				result.exp--;
				if (result.exp < 0)
					return new BigInt ();
			}
			return result;
		}

		//Вычитание (BigInt, BigInt)
		public static BigInt operator -(BigInt left, BigInt right) {
			BigInt result = new BigInt (left.value, left.exp);
			BigInt _right = new BigInt (right.value, right.exp);
			while (result.exp > _right.exp) {
				_right.value /= 10;
				_right.exp++;
			}
			while (result.exp < _right.exp) {
				result.value /= 10;
				result.exp++;
			}
			result.value -= _right.value;
			while (result.value >= 10) {
				result.value /= 10;
				result.exp++;
			}
			while (result.value < 1) {
				result.value *= 10;
				result.exp--;
				if (result.exp < 0)
					return new BigInt ();
			}
			return result;
		}

		//Умножение (BigInt, float)
		public static BigInt operator *(BigInt left, float right) {
			BigInt result = new BigInt (left.value, left.exp);
			result.value *= right;
			while (result.value >= 10) {
				result.value /= 10;
				result.exp++;
			}
			while (result.value < 1) {
				result.value *= 10;
				result.exp--;
				if (result.exp < 0)
					return new BigInt ();
			}
			return result;
		}

		//Умножение (BigInt, BigInt)
		public static BigInt operator *(BigInt left, BigInt right) {
			BigInt result = new BigInt (left.value, left.exp);
			result.exp += right.exp;
			result.value *= right.value;
			while (result.value >= 10) {
				result.value /= 10;
				result.exp++;
			}
			while (result.value < 1) {
				result.value *= 10;
				result.exp--;
				if (result.exp < 0)
					return new BigInt ();
			}
			return result;
		}

		//Сравнение (>)
		public static bool operator >(BigInt left, BigInt right) {
			if (left.exp > right.exp)
				return true;
			else if (left.exp < right.exp)
				return false;
			else {
				if (left.value > right.value)
					return true;
				else
					return false;
			}
		}

		//Сравнение (<)
		public static bool operator <(BigInt left, BigInt right) {
			if (left.exp < right.exp)
				return true;
			else if (left.exp > right.exp)
				return false;
			else {
				if (left.value < right.value)
					return true;
				else
					return false;
			}
		}

		//Сравнение (>=)
		public static bool operator >=(BigInt left, BigInt right) {
			if (left.exp > right.exp)
				return true;
			else if (left.exp < right.exp)
				return false;
			else {
				if (left.value >= right.value)
					return true;
				else
					return false;
			}
		}

		//Сравнение (<=)
		public static bool operator <=(BigInt left, BigInt right) {
			if (left.exp < right.exp)
				return true;
			else if (left.exp > right.exp)
				return false;
			else {
				if (left.value <= right.value)
					return true;
				else
					return false;
			}
		}

		public static explicit operator float(BigInt bint) {
			return Mathf.Clamp(
				bint.exp > 0 ? bint.value * Mathf.Pow(10, bint.exp) : bint.value, 
				-Mathf.Infinity, Mathf.Infinity);
		}

		public override string ToString ()
		{
			char[] prefixes = new char[] { 'K', 'M', 'B', 'T', 'q', 'Q', 's', 'S', 'O', 'N', 'd', 'U', 'D', '!', '@', '#', '$', '%', '^', '&', '*' };
			if (exp < 3)
				return string.Format ("{0}", Mathf.RoundToInt(value * Mathf.Pow (10, exp)));
			else {
				int exp_idx = Mathf.RoundToInt((exp - (exp % 3)) / 3) - 1;
				if (exp_idx > prefixes.Length - 1)
					exp_idx = prefixes.Length - 1;
				return string.Format ("{0:0.00}{1}", value * Mathf.Pow (10, exp % 3), prefixes[exp_idx]);
			}
		}
	}
}
