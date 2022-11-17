using Unit05.Game.Casting;
using Unit05.Game.Services;

namespace Unit05.Game.Scripting{

    public class Clock : Action{
        //Is a clock that keeps track of the game's countdown
        private int _tickCount;
        public Clock(){
            _timer = 60;
            _tickCount = 0;
        }

        public void Tick(Cast cast){
            //every 12 ticks is a second of real time
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