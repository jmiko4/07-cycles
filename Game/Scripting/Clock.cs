using Unit05.Game.Casting;
using Unit05.Game.Services;

namespace Unit05.Game.Scripting{

    public class Clock : Action{
        //Is a clock that keeps track of the number of ticks and increases the length of the Snakes
        private int _tickCount;
        public Clock(){
            _tickCount = 0;
        }

        public void Tick(Cast cast){
            //every 5 ticks increases the length of the snakes
            Snake snake = (Snake)cast.GetFirstActor("snake");
            Snake2 snake2 = (Snake2)cast.GetFirstActor("snake2");
            
            _tickCount++;
            if (_tickCount == 5){
                _tickCount = 0;
                snake.GrowTail(1);
                snake2.GrowTail(1);
            }
        }

        public void Execute(Cast cast, Script script){
            Tick(cast);
        }

        


    }


}