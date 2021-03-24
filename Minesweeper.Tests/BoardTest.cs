using NUnit.Framework;

namespace Minesweeper.Tests
{
    public class BoardTest
    {
        [TestCase(1, 2)]
        [TestCase(4, 1)]
        [TestCase(5, 4)]
        public void GetDimensions_ReturnsCorrectDimensions(int numberOfRows, int numberOfColumns)
        {
            var board = new Board(new char[numberOfRows, numberOfColumns]);
            Assert.That(board.NumberOfRows, Is.EqualTo(numberOfRows));
            Assert.That(board.NumberOfColumns, Is.EqualTo(numberOfColumns));
        }

        [Test]
        public void PositionGetter_ReturnsCorrectValue()
        {
            var expectedValues = new[,] {{'1', '2', '3'}, {'4', '5', '6'}};
            var board = new Board(expectedValues);

            for (var x = 0; x < board.NumberOfRows; x++)
            {
                for (var y = 0; y < board.NumberOfRows; y++)
                {
                    Assert.That(board[x + 1, y + 1], Is.EqualTo(expectedValues[x, y]));
                }
            }
        }


        [Test]
        public void Equal_GivenDifferentBoards_DoesNotEqual()
        {
            var board1 = new Board(new[,] {{'1', '2', '3'}, {'4', '5', '6'}});
            var board2 = new Board(new[,] {{'1', '2', '3'}, {'4', '3', '6'}});

            Assert.That(board1, Is.Not.EqualTo(board2));
        }

        [Test]
        public void Equal_GivenSameBoards_DoesEqual()
        {
            var values = new[,] {{'1', '2', '3'}, {'4', '5', '6'}};
            var board1 = new Board(values);
            var board2 = new Board(values);

            Assert.That(board1, Is.EqualTo(board2));
        }
    }
}