using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool _isGameOver = false;
        private int winner;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (_isGameOver == false)
            {
                HandleFoodCollisions(cast);
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleFoodCollisions(Cast cast)
        {
            Snake snake = (Snake)cast.GetFirstActor("snake");
            Snake2 snake2 = (Snake2)cast.GetFirstActor("snake2");
            Score score = (Score)cast.GetFirstActor("score");
            Food food = (Food)cast.GetFirstActor("food");
            
            if (snake.GetHead().GetPosition().Equals(food.GetPosition()))
            {
                int points = food.GetPoints();
                snake.GrowTail(points);
                score.AddPoints(points);
                food.Reset();
            }

            if (snake2.GetHead().GetPosition().Equals(food.GetPosition()))
            {
                int points = food.GetPoints();
                snake2.GrowTail(points);
                score.AddPoints(points);
                food.Reset();
            }
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Snake snake = (Snake)cast.GetFirstActor("snake");
            Snake2 snake2 = (Snake2)cast.GetFirstActor("snake2");
            Actor head = snake.GetHead();
            Actor head2 = snake2.GetHead();
            List<Actor> body = snake.GetBody();
            List<Actor> body2 = snake2.GetBody();

            foreach (Actor segment in body)
            {
                if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    _isGameOver = true;
                    winner = 1;

                }
            }

            foreach (Actor segment2 in body2)
            {
                if (segment2.GetPosition().Equals(head.GetPosition()))
                {
                    _isGameOver = true;
                    winner = 2;
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (_isGameOver == true)
            {
                Script script = new Script();
                Snake snake = (Snake)cast.GetFirstActor("snake");
                Snake2 snake2 = (Snake2)cast.GetFirstActor("snake2");
                List<Actor> segments = snake.GetSegments();
                List<Actor> segments2 = snake2.GetSegments();
                Food food = (Food)cast.GetFirstActor("food");

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over!");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                //make everything white
                if (winner == 2){
                    foreach (Actor segment in segments)
                    {
                        segment.SetColor(Constants.WHITE);
                        message.SetText("Green Snake Wins");
                    }
                } 
                else{
                    foreach (Actor segment in segments2)
                    {
                        segment.SetColor(Constants.WHITE);
                        message.SetText("Red Snake Wins");
                    }
                }
                
                food.SetColor(Constants.WHITE);
            }
        }

    }
}