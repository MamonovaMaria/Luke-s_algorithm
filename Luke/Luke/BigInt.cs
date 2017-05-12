using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luke
{
	enum Sign
	{
		plus,
		minus
	}

	enum DivisionMode
	{
		quotient,
		rest
	}

	/// <summary>
	/// натуральные числа произвольной длины
	/// </summary>
	// правильно сравнивает числа (по модулю) и находит сумму, разность, произведение, частное и остаток от деления
	// есть конструктор копирования, что даёт возможность присваивать экземпляры
	// удобный конструктор от строки (просто строка с любыми символами, 
	//		символ '-' в начале интерпретируется как минус, остальные символы-не-цифры игнорируются)
	class BigInt
	{
		Sign sign;
		Queue<int> module;

		BigInt()
		{
			sign = Sign.plus;
			module = new Queue<int>();
		}

		public BigInt(BigInt a)
		{
			sign = a.sign;
			module = new Queue<int>(a.module);
		}

		public bool IsNegative()
		{
			if (sign == Sign.minus && !(module.Count == 1 && module.First() == 0))
				return true;
			else
				return false;
		}

		public BigInt(string text)
		{
			if (text[0] == '-')
				sign = Sign.minus;
			else
				sign = Sign.plus;

			module = new Queue<int>();

			for (int i=0; i<text.Count(); i++)
				if (Char.IsDigit(text[i]))
					module.Enqueue(Int32.Parse(text[i].ToString()));

			UnNull();
		}

		public BigInt(int number)
		{
			BigInt temp = new BigInt(number.ToString());
			sign = temp.sign;
			module = temp.module;
		}

		BigInt UnNull()
		{
			while (module.Count() > 1)
				if (module.ElementAt(0) != 0)
					break;
				else
					module.Dequeue();
			return this;
		}

		public static bool operator < (BigInt left, BigInt right)
		{
			if (left.module.Count() < right.module.Count())
				return true;
			if (left.module.Count() > right.module.Count())
				return false;

			for (int i = 0; i < left.module.Count(); i++)
				if (left.module.ElementAt(i) < right.module.ElementAt(i))
					return true;
				else if (left.module.ElementAt(i) > right.module.ElementAt(i))
					return false;

			return false;
		}

		public static bool operator < (BigInt left, int right)
		{
			return left < new BigInt(right);
		}

		public static bool operator == (BigInt left, BigInt right)
		{
			if (left.module.Count() != right.module.Count())
				return false;

			for (int i = 0; i < left.module.Count(); i++)
				if (left.module.ElementAt(i) != right.module.ElementAt(i))
					return false;

			return true;
		}

		public static bool operator == (BigInt left, int right)
		{
			return left == new BigInt(right);
		}

		public static bool operator <= (BigInt left, BigInt right)
		{
			return (left < right || left == right);
		}

		public static bool operator <= (BigInt left, int right)
		{
			return left <= new BigInt(right);
		}

		public static bool operator > (BigInt left, BigInt right)
		{
			return !(left <= right);
		}

		public static bool operator > (BigInt left, int right)
		{
			return left > new BigInt(right);
		}

		public static bool operator != (BigInt left, BigInt right)
		{ 
			return !(left == right);
		}

		public static bool operator != (BigInt left, int right)
		{
			return left != new BigInt(right);
		}

		public static bool operator >= (BigInt left, BigInt right)
		{
			return (left > right || left == right);
		}

		public static bool operator >= (BigInt left, int right)
		{
			return left >= new BigInt(right);
		}

		public static BigInt operator + (BigInt left, BigInt right)
		{
			if (left.sign != right.sign) // если разные знаки, то отправляем на метод разность
				if (left.sign == Sign.minus) // заменяем –x+y на y-x
				{
					BigInt tmp = new BigInt(left);
					tmp.sign = Sign.plus;
					return right - tmp;
				}
				else // заменяем x+-y на x-y
				{
					BigInt tmp = new BigInt(right);
					right.sign = Sign.plus;
					return left - tmp;
				}

			BigInt summa = new BigInt(); // сюда записывается результат
			int digit = 0; // 1 для добавления к старшему разряду
			int marker = 0; // для вычисления позиции, с которой остаются разряды только одного числа

			if (left.module.Count() >= right.module.Count()) // ставим большее число на первое место
			{
				for (int i = left.module.Count() - 1, j = right.module.Count() - 1; j >= 0; i--, j--) // начиная с первых разрядов складываем числа
				{
					summa.module.Enqueue((left.module.ElementAt(i) + right.module.ElementAt(j) + digit) % 10);

					if ((left.module.ElementAt(i) + right.module.ElementAt(j) + digit) >= 10)
						digit = 1;
					else
						digit = 0; // прибавляем 1 на следующем шаге, если сумма больше 10

					marker = i;
				}

				for (int i = marker - 1; i >= 0; i--) // начиная с позиции метки добиваем цифрами из большего числа, учитывая возможное прибавление 1
				{
					summa.module.Enqueue((left.module.ElementAt(i) + digit) % 10);

					if ((left.module.ElementAt(i) + digit) == 10)
						digit = 1;
					else
						digit = 0;
				}

				if (digit == 1)
					summa.module.Enqueue(1); // срабатывает в случае когда увеличивается разряд, например 99+1=100
			}
			else
			{
				for (int i = right.module.Count() - 1, j = left.module.Count() - 1; j >= 0; i--, j--)
				{
					summa.module.Enqueue((right.module.ElementAt(i) + left.module.ElementAt(j) + digit) % 10);

					if ((right.module.ElementAt(i) + left.module.ElementAt(j) + digit) >= 10)
						digit = 1;
					else
						digit = 0;

					marker = i;
				}

				for (int i = marker - 1; i >= 0; i--)
				{
					summa.module.Enqueue((right.module.ElementAt(i) + digit) % 10);

					if ((right.module.ElementAt(i) + digit) == 10)
						digit = 1;
					else
						digit = 0;
				}

				if (digit == 1) summa.module.Enqueue(1);
			}

			summa.sign = left.sign;
			summa.module = new Queue<int>(summa.module.Reverse());

			return summa;
		}

		public static BigInt operator + (BigInt left, int right)
		{
			return left + new BigInt(right);
		}

		public static BigInt operator - (BigInt left, BigInt right)
		{
			BigInt tmp = new BigInt(right);
			if (tmp.sign == Sign.minus)
				tmp.sign = Sign.plus; // x-(-y) преобразуем в x+y и передаем в метод суммы
			else tmp.sign = Sign.minus;

			if (left.sign == tmp.sign)
				return left + tmp; // –x-y преобразуем в –(x+y) передаем методу суммы

			BigInt rasn = new BigInt(); // сюда записывается разность
			int digit = 0; // 1 для вычитания из старшего разряда
			int marker = 0; // для вычисления позиции, с которой остаются разряды только одного числа

			if (left >= right) // ставим большее число сверху в столбике
			{
				for (int i = left.module.Count() - 1, j = right.module.Count() - 1; j >= 0; i--, j--)
				{
					if ((left.module.ElementAt(i) - right.module.ElementAt(j) - digit) >= 0) // поразрядно вычитаем
					{
						rasn.module.Enqueue(left.module.ElementAt(i) - right.module.ElementAt(j) - digit);
						digit = 0;
					}
					else
					{
						rasn.module.Enqueue(left.module.ElementAt(i) - right.module.ElementAt(j) + 10 - digit); // заимствуем 1 из старшего разряда
						digit = 1;
					}

					marker = i;
				}

				for (int i = marker - 1; i >= 0; i--) // добиваем числами оставшихся разрядов, учитывая -1
				{
					rasn.module.Enqueue(Math.Abs((left.module.ElementAt(i) - digit + 10) % 10));

					if ((digit == 1) && (left.module.ElementAt(i) - digit) < 0)
						digit = 1;
					else
						digit = 0;
				}
				rasn.sign = left.sign;
			}
			else
			{
				for (int i = right.module.Count() - 1, j = left.module.Count() - 1; j >= 0; i--, j--)
				{
					if ((right.module.ElementAt(i) - left.module.ElementAt(j) - digit) >= 0)
					{
						rasn.module.Enqueue(right.module.ElementAt(i) - left.module.ElementAt(j) - digit);
						digit = 0;
					}
					else
					{
						rasn.module.Enqueue(right.module.ElementAt(i) - left.module.ElementAt(j) + 10 - digit);
						digit = 1;
					}

					marker = i;
				}

				for (int i = marker - 1; i >= 0; i--)
				{
					rasn.module.Enqueue(Math.Abs((right.module.ElementAt(i) - digit + 10) % 10));

					if ((digit == 1) && (right.module.ElementAt(i) - digit) < 0)
						digit = 1;
					else
						digit = 0;
				}

				rasn.sign = tmp.sign;
			}

			rasn.module = new Queue<int>(rasn.module.Reverse());
			return rasn.UnNull();
		}

		public static BigInt operator - (BigInt left, int right)
		{
			return left - new BigInt(right);
		}
		
		public static BigInt operator * (BigInt left, BigInt right)
		{
			BigInt result = new BigInt("0");

			for (int i = right.module.Count - 1; i >= 0; i--)
			{
				int digit = 0;
				BigInt temp = new BigInt();
				for (int j = left.module.Count - 1; j >= 0; j--)
				{
					temp.module.Enqueue((left.module.ElementAt(j) * right.module.ElementAt(i) + digit) % 10);
					digit = (left.module.ElementAt(j) * right.module.ElementAt(i) + digit) / 10;
				}
				temp.module.Enqueue(digit);
				temp.module = new Queue<int>(temp.module.Reverse());
				for (int j = i; j < right.module.Count - 1; j++)
					temp.module.Enqueue(0);
				result += temp;
			}

			if (right.sign != left.sign)
				result.sign = Sign.minus;
			else
				result.sign = Sign.plus;

			return result.UnNull();
		}

		public static BigInt operator * (BigInt left, int right)
		{
			return left * new BigInt(right);
		}

		public static BigInt operator / (BigInt left, BigInt right)
		{
			return Division(left, right, DivisionMode.quotient);
		}

		public static BigInt operator / (BigInt left, int right)
		{
			return left / new BigInt(right);
		}

		public static BigInt operator % (BigInt left, BigInt right)
		{
			return Division(left, right, DivisionMode.rest);
		}

		public static BigInt operator %(BigInt left, int right)
		{
			return left % new BigInt(right);
		}

		/// <summary> Возведение в степень по модулю, сложность O(module^2) </summary> 
		public static BigInt Pow(BigInt basis, BigInt _rank, BigInt module)
		{
			BigInt a = basis;
			BigInt rank = _rank;
			BigInt b = new BigInt("1");
			while (rank > new BigInt("0"))
			{

				BigInt r = rank % new BigInt("2");
				BigInt q = rank / new BigInt("2");
				if (r == new BigInt("0"))
				{
					rank = q;
					a = (a * a) % module;
				}
				else
				{
					rank -= new BigInt("1");
					b = (b * a) % module;
				}
			}
			return b;
		}

		public override string ToString()
		{
			StringBuilder str = new StringBuilder();
			if (module.Count() == 1 && module.First() == 0 && sign == Sign.minus)
				sign = Sign.plus;

			if (sign == Sign.minus)
				str.Append('-');

			for (int i = 0; i < module.Count(); i++)
				str.Append(module.ElementAt(i));

			return str.ToString();
		}
		
		static BigInt Division(BigInt divident, BigInt _divider, DivisionMode mode)
		{
			BigInt divider = new BigInt(_divider);
			divider.sign = Sign.plus;
			BigInt quotient = new BigInt();
			BigInt rest = new BigInt();
			BigInt tmp = new BigInt();

			tmp.module = divider.module;
			for (int i = 0; i < divident.module.Count(); i++)
			{
				rest.module.Enqueue(divident.module.ElementAt(i)); // промежуточный остаток
				rest.UnNull();
																   // пока промежуточный остаток меньше делителя, пишем к частному 0
				if (rest >= divider)
					for (int j = 1; j <= 10; j++) // цикл, формирующий цифры частного
					{
						if (rest < tmp) // промежуточный остаток меньше делителя*j
						{
							quotient.module.Enqueue(j - 1);
							rest -= (tmp - divider);
							tmp.module = divider.module;
							break;
						}
						if (rest == tmp) // промежуточный остаток кратный делителю
						{
							quotient.module.Enqueue(j);
							rest.module.Clear();
							tmp.module = divider.module;
							break;
						}
						tmp += divider; // прибавляем сам делитель, пока не станет больше остатка
					}
				else
					quotient.module.Enqueue(0);
			} // цифры делимого заканчиваются и остаток меньше делимого, цикл завершается

			if (divident.sign != _divider.sign)
			{
				quotient.sign = Sign.minus;
				rest.sign = Sign.minus;
			}

			if (rest.module.Count() == 0)
			{
				rest.module.Enqueue(0);
				rest.sign = Sign.plus;
			}
			if (quotient.module.Count() == 0)
			{
				quotient.module.Enqueue(0);
				rest.sign = Sign.plus;
			}

			if (mode == DivisionMode.quotient)
				return quotient.UnNull();
			else
				return rest.UnNull();
		}

		/// <summary> НОД, сложность O(max(number1, number2) ^2) </summary>
		public static BigInt GCD(BigInt number1, BigInt number2)
		{
			BigInt a = new BigInt(number1);
			BigInt b = new BigInt(number2), x = new BigInt(0), d = new BigInt(1);
			while (a != 0)
			{
				BigInt q = b / a;
				BigInt y = a;
				a = b % a;
				b = y;
				y = d;
				d = x - q * d;
				x = y;
			}
			return b;
		}

	}


}
