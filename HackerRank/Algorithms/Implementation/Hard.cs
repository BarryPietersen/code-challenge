using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Algorithms.Implementation
{
    public static class Hard
    {
        /*
            runtime: o(m * n)
              space: o(log n)
        */
        // https://www.hackerrank.com/challenges/matrix-rotation-algo/problem
        public static List<List<int>> matrixRotation(List<List<int>> matrix, int rotation)
        {
            if (Math.Min(matrix.Count, matrix[0].Count) % 2 != 0)
                throw new Exception("the matrix contains an inner most layer which cannot be rotated");

            if (!matrix.TrueForAll(lst => lst.Count == matrix[0].Count))
                throw new Exception("the matrix dimensions are not all equal in length");

            if (rotation == 0) return matrix;

            // top left bottom right indexes
            int t = 0;
            int l = 0;
            int b = matrix.Count - 1;
            int r = matrix[0].Count - 1;

            // vertical and horizotal
            // lengths of each layer
            int v = matrix.Count;
            int h = matrix[0].Count;

            // constraint: min(m,n) % 2 == 0,
            // this guarantees the inner most 
            // layer always has two dimensions
            // layercount - the number of layers in the matrix
            int layercount = Math.Min(matrix.Count(), matrix[0].Count) / 2;

            int _r, idx, i;
            int[] layer;

            while (layercount > 0)
            {
                // mod the number of rotations
                // by length of the layer
                _r = rotation % ((v + h) * 2 - 4);

                // only do work to the layer
                // if there is work to be done
                if (_r > 0)
                {
                    idx = 0;
                    layer = new int[(v + h) * 2 - 4];

                    for (i = 1; i < v; i++)
                        layer[idx++] = matrix[t + i][l];

                    for (i = 1; i < h; i++)
                        layer[idx++] = matrix[b][l + i];

                    for (i = 1; i < v; i++)
                        layer[idx++] = matrix[b - i][r];

                    for (i = 1; i < h; i++)
                        layer[idx++] = matrix[t][r - i];

                    layer = rotateArray(layer, _r);
                    idx = 0;

                    for (i = 1; i < v; i++)
                        matrix[t + i][l] = layer[idx++];

                    for (i = 1; i < h; i++)
                        matrix[b][l + i] = layer[idx++];

                    for (i = 1; i < v; i++)
                        matrix[b - i][r] = layer[idx++];

                    for (i = 1; i < h; i++)
                        matrix[t][r - i] = layer[idx++];
                }

                v -= 2; h -= 2;
                t++; l++; b--; r--; layercount--;
            }

            return matrix;
        }

        private static int[] rotateArray(int[] arr, int r)
        {
            r %= arr.Length;

            int[] result = new int[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                result[r] = arr[i];
                r = (r + 1) == arr.Length ? 0 : r + 1;
            }

            return result;
        }
    }
}
