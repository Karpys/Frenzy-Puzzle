namespace PuzzleFrenzy.Scripts
{
    using UnityEngine;

    public class PuzzleGenerator
    {
        private Vector2Int m_PuzzleSize = Vector2Int.zero;
        private int[][][] m_PuzzleData = null;

        public int[][][] PuzzleData => m_PuzzleData;
        public PuzzleGenerator(Vector2Int size)
        {
            m_PuzzleSize = size;
            m_PuzzleData = new int[size.x][][];

            for (int i = 0; i < m_PuzzleData.Length; i++)
            {
                m_PuzzleData[i] = new int[size.y][];
            }

            for (int i = 0; i < m_PuzzleData.Length; i++)
            {
                for (int j = 0; j < m_PuzzleData[i].Length; j++)
                {
                    m_PuzzleData[i][j] = new int[4];
                }
            }
            
            GenerateRandomPuzzle();
        }

        private void GenerateRandomPuzzle()
        {
            for (int x = 0; x < m_PuzzleData.Length; x++)
            {
                for (int y = 0; y < m_PuzzleData[x].Length; y++)
                {
                    bool rightCheck = x != m_PuzzleData.Length - 1;
                    bool leftCheck = x != 0;
                    bool topCheck = y != m_PuzzleData[x].Length - 1;
                    bool botCheck = y != 0;

                    if (topCheck)
                    {
                        m_PuzzleData[x][y][0] = GetPuzzleValue(m_PuzzleData[x][y+1][2]);
                    }
                    
                    if (rightCheck)
                    {
                        m_PuzzleData[x][y][1] = GetPuzzleValue(m_PuzzleData[x + 1][y][3]);
                    }

                    if (botCheck)
                    {
                        m_PuzzleData[x][y][2] = GetPuzzleValue(m_PuzzleData[x][y-1][0]);
                    }

                    if (leftCheck)
                    {
                        m_PuzzleData[x][y][3] = GetPuzzleValue(m_PuzzleData[x - 1][y][1]);
                    }
                }
            }
        }

        private int GetPuzzleValue(int value)
        {
            if (value == 0)
            {
                if (Random.Range(0, 2) == 0)
                    return 1;
                else
                {
                    return -1;
                }
            }

            return -value;
        }
    }
}