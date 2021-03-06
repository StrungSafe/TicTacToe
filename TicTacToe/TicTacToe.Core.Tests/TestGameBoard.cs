﻿namespace TicTacToe.Core.Tests
{
    using NSubstitute;

    using NUnit.Framework;

    using TicTacToe.Core.Interfaces;

    [TestFixture]
    public class TestGameBoard
    {
        [SetUp]
        public void SetUp()
        {
            gameSettingsMock = Substitute.For<IGameSettings>();
            gameSettingsMock.Size.Returns(3);

            systemUnderTest = new GameBoard(gameSettingsMock);
        }

        private IGameSettings gameSettingsMock;

        private IGameBoard systemUnderTest;

        [Test]
        public void BoardGetter_WhenInvoked_ReturnsCopyOfBoard()
        {
            systemUnderTest.Board[1, 1] = GameBoardMark.X;

            Assert.That(systemUnderTest.Board[1, 1], Is.EqualTo(GameBoardMark.Empty));
        }

        [Test]
        public void Clear_WhenInvoked_ClearsBoard()
        {
            systemUnderTest.PlaceMarker(new Move(Player.X, 1, 1));
            systemUnderTest.Clear();

            Assert.That(systemUnderTest.Board.GetLength(0), Is.EqualTo(3));
            Assert.That(systemUnderTest.Board.GetLength(1), Is.EqualTo(3));

            foreach (GameBoardMark boardMark in systemUnderTest.Board)
            {
                Assert.That(boardMark, Is.EqualTo(GameBoardMark.Empty));
            }
        }

        [Test]
        public void Constructor_WhenInvoked_CreatesEmptyBoard()
        {
            Assert.That(systemUnderTest.Board.GetLength(0), Is.EqualTo(3));
            Assert.That(systemUnderTest.Board.GetLength(1), Is.EqualTo(3));

            foreach (GameBoardMark boardMark in systemUnderTest.Board)
            {
                Assert.That(boardMark, Is.EqualTo(GameBoardMark.Empty));
            }
        }

        [Test]
        public void PlaceMarker_WhenInvoked_PlacesMarker()
        {
            var move = new Move(Player.X, 1, 1);

            systemUnderTest.PlaceMarker(move);

            Assert.That(systemUnderTest.Board[1, 1], Is.EqualTo(GameBoardMark.X));
        }
    }
}