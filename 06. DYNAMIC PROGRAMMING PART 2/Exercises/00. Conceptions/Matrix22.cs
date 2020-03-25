namespace _00._Conceptions
{
    using System;
    using System.Numerics;
    using System.Text;

    public class Matrix22
    {
        private static Matrix22 _identityMatrix;
        private readonly BigInteger[,] _matrix;

        public Matrix22(BigInteger a, BigInteger b, BigInteger c, BigInteger d)
        {
            this._matrix = new BigInteger[2, 2];

            this._matrix[0, 0] = a;
            this._matrix[0, 1] = b;
            this._matrix[1, 0] = c;
            this._matrix[1, 1] = d;
        }

        public BigInteger TopLeft => this._matrix[0, 0];

        public Matrix22 MultiplyTo(Matrix22 other)
        {
            var result = new Matrix22(0, 0, 0, 0);

            result._matrix[0, 0] =
                this._matrix[0, 0] * other._matrix[0, 0] 
                + this._matrix[0, 1] * other._matrix[1, 0];

            result._matrix[0, 1] =
                this._matrix[0, 0] * other._matrix[0, 1] 
                + this._matrix[0, 1] * other._matrix[1, 1];

            result._matrix[1, 0] =
                this._matrix[1, 0] * other._matrix[0, 0] 
                + this._matrix[1, 1] * other._matrix[1, 0];

            result._matrix[1, 1] =
                this._matrix[1, 0] * other._matrix[0, 1] 
                + this._matrix[1, 1] * other._matrix[1, 1];

            return result;
        }

        public Matrix22 Square()
        {
            return this.MultiplyTo(this);
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            for (var row = 0; row < this._matrix.GetLength(0); row++)
            {
                for (var col = 0; col < this._matrix.GetLength(1); col++)
                {
                    result.Append(this._matrix[row, col].ToString().PadLeft(5));
                }

                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }

        public static Matrix22 IdentityMatrix()
        {
            if (_identityMatrix == null)
            {
                _identityMatrix = new Matrix22(1, 0, 0, 1);
            }

            return _identityMatrix;
        }
    }
}
