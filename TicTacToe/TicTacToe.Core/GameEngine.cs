﻿namespace TicTacToe.Core
{
    using System;
    using Interfaces;

    public class GameEngine : IGameEngine
    {
        private readonly IGameBoard gameBoard;

        private readonly IGameBoardAnalyzer gameBoardAnalyzer;

        private readonly IMoveValidator moveValidator;

        public GameEngine(IGameBoard gameBoard, IMoveValidator moveValidator, IGameBoardAnalyzer gameBoardAnalyzer)
        {
            this.gameBoard = gameBoard ?? throw new ArgumentNullException(nameof(gameBoard));
            this.moveValidator = moveValidator ?? throw new ArgumentNullException(nameof(moveValidator));
            this.gameBoardAnalyzer = gameBoardAnalyzer ?? throw new ArgumentNullException(nameof(gameBoardAnalyzer));

            NewGame();
        }

        public GameBoardMark[,] GameBoard => gameBoard.GameBoard;

        public GameState GameState { get; private set; }

        public bool MakeMove()
        {
            if (!moveValidator.IsValidMove())
            {
                return false;
            }

            gameBoard.PlaceMarker();

            GameBoardState gameBoardState = gameBoardAnalyzer.AnalyzeGameBoard();

            UpdateGameState(gameBoardState);

            return true;
        }

        public void NewGame()
        {
            gameBoard.Clear();

            GameState = GameState.NewGameXMove;
        }

        private void UpdateGameState(GameBoardState newGameBoardState)
        {
            if (GameState == GameState.NewGameXMove || GameState == GameState.XMove)
            {
                GameState = GameState.OMove;
            }
            else if (GameState == GameState.OMove)
            {
                GameState = GameState.XMove;
            }

            if (newGameBoardState == GameBoardState.Tie)
            {
                GameState = GameState.Tie;
            }
            else if (newGameBoardState == GameBoardState.XWinner)
            {
                GameState = GameState.XWinner;
            }
            else if (newGameBoardState == GameBoardState.OWinner)
            {
                GameState = GameState.OWinner;
            }
        }
    }
}