using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Media;

namespace SoccerFoos
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum GameState
        {
            Logo,
            TitleScreen,
            MenuScreen,
            PlayerONePlaying,
            PlayerTwoPlaying,
            Playing,
            Observe,
            Puased
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Textures For Our Menus
        private Texture2D startButton;
        private Texture2D SinglePlayer;
        private Texture2D PlayerOneBut;
        private Texture2D PlayerTwoBut;
        private Texture2D MultiPlayer;
        private Texture2D Observe;
        private Texture2D exitButton;
        private Texture2D resumeButton;
        private Texture2D PauseButton;
        private Texture2D ReturnMainMenu;

        private Texture2D LogoScreen;
        private Texture2D Title;
        private Texture2D loadingtext;
        Rectangle StartRect;
        Rectangle ExitRect;
        Rectangle ResumeRect;
        Rectangle ReturnRect;
        Rectangle PuasedRect;
        Rectangle PlayerOneButRec;
        Rectangle PlayerTwoButRect;
        Rectangle MultiplayerRect;
        Rectangle ObserveRect;
        Vector2 StartButtPos;
        Vector2 ExitButtPos;
        Vector2 ResumeButtPos;
        Vector2 ReturnButtPos;
        Vector2 PuasedPos;
        Vector2 SinglePos;
        Vector2 PlayerOneButPos;
        Vector2 PlayerTwoButPos;

        Vector2 LogoPos;
        Vector2 MultiplayerPos;
        Vector2 Observepos;


        MouseState mouseState;
        MouseState previousMouseState;
        private GameState gamestate;

        

        //Custom Variables.
        //Keeping Score
        int PlayerOneScore = 0;
        int PlayerTwoScore = 0;
        float timer = 300;
        float SwitchTime = 5000;
        SpriteFont ScoreBoard;
        Texture2D ScoreDisplay;

        //Background variables
        Texture2D Background;

        //Paddles
        Texture2D Paddle;

        //Player One Variables
        Vector2 PaddleOnePos;
        Rectangle PaddleOneRect;

        //Player Two Variables
        Vector2 PaddleTwoPos;
        Rectangle PaddleTwoRect;

        //Ball
        Texture2D Ball;
        Vector2 BallPos;
        Vector2 BallSpeed;
        Rectangle BallRect;

        //Goals
        Texture2D Goal;
        Vector2 GoalOnePos;
        Rectangle GoalOneRect;
        Vector2 GoalTwoPos;
        Rectangle GoalTwoRect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //Set the screen size
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;
            LogoPos = new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, 200);
            gamestate = GameState.Logo;
            mouseState = Mouse.GetState();
            previousMouseState = mouseState;
            



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Load texture for Menus
            startButton = Content.Load<Texture2D>("start");
            exitButton = Content.Load<Texture2D>("Quit");
            resumeButton = Content.Load<Texture2D>("resume");
            ReturnMainMenu = Content.Load<Texture2D>("return");

            LogoScreen = Content.Load<Texture2D>("SouthStarLogo");
            PauseButton = Content.Load<Texture2D>("Paused");
            Title = Content.Load<Texture2D>("PSoccerTitle");
            // SinglePlayer = Content.Load<Texture2D>("SinglePlayer");
            MultiPlayer = Content.Load<Texture2D>("Multiplayer");
            Observe = Content.Load<Texture2D>("Observe");
            loadingtext = Content.Load<Texture2D>("loading");
            PlayerOneBut = Content.Load<Texture2D>("1PButton");
            PlayerTwoBut = Content.Load<Texture2D>("2PButton");
            StartButtPos = new Vector2(250, 300);
            ExitButtPos = new Vector2(600, 300);
            ReturnButtPos = new Vector2(250, 500);
            SinglePos = new Vector2(250, 100);
            PlayerOneButPos = new Vector2(250, 200);
            PlayerTwoButPos = new Vector2(250, 300);
            //PlayertwoSinglePos = new Vector2(350,300);
            MultiplayerPos = new Vector2(250, 400);
            Observepos = new Vector2(250, 500);
            PuasedPos = new Vector2(250, 0);
            ResumeButtPos = new Vector2(250, 400);
            //ScroeBoard Display
            ScoreDisplay = Content.Load<Texture2D>("ScoreBoard");
            ScoreBoard = Content.Load<SpriteFont>("ScoreFont");
            //Load the Background image
            Background = Content.Load<Texture2D>("Field");

            //Load the Paddle image
            Paddle = Content.Load<Texture2D>("Paddle");

            //Paddle One initialization
            PaddleOnePos = new Vector2(130, 195);
            PaddleOneRect = new
                Rectangle((int)PaddleOnePos.X, (int)PaddleOnePos.Y,
                            Paddle.Width, Paddle.Height);

            //Paddle Two initialization
            PaddleTwoPos = new Vector2(950 - 70 - Paddle.Width, 195);
            PaddleTwoRect = new
                Rectangle((int)PaddleTwoPos.X, (int)PaddleTwoPos.Y,
                            Paddle.Width, Paddle.Height);

            //Load the Ball image
            Ball = Content.Load<Texture2D>("soccerball-png-25");
            //Initialize all of the ball variables
            BallPos = new Vector2(500 - (Ball.Width / 2),
                                   250 - (Ball.Height / 2));
            BallSpeed = new Vector2(4f, 0.5f);
            BallRect = new Rectangle((int)BallPos.X, (int)BallPos.Y,
                                        Ball.Width, Ball.Height);

            //Goals
            Goal = Content.Load<Texture2D>("Goal");
            GoalOnePos = new Vector2(0, 270);
            GoalOneRect = new Rectangle((int)GoalOnePos.X, (int)GoalOnePos.Y, Goal.Width, Goal.Height);
            GoalTwoPos = new Vector2(948, 270);
            GoalTwoRect = new Rectangle((int)GoalTwoPos.X, (int)GoalTwoPos.Y, Goal.Width, Goal.Height);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            //Start Game Clock when game play starts
            if (gamestate == GameState.PlayerONePlaying || gamestate == GameState.PlayerTwoPlaying || gamestate == GameState.Playing || gamestate == GameState.Observe)
            {

                if (timer > 0)
                {
                    timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                }
                else
                {

                    gamestate = GameState.MenuScreen;
                }
            }






            if (gamestate == GameState.Logo)
            {

                if (SwitchTime > 0)
                {
                    SwitchTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                }
                else
                {

                    gamestate = GameState.MenuScreen;
                }
            }



            if (gamestate == GameState.PlayerONePlaying)
            {
                if (BallPos.X > 500)
                {


                    if (BallPos.Y > PaddleTwoPos.Y)
                    {
                        PaddleTwoPos.Y = PaddleTwoPos.Y + 10;
                        PaddleTwoRect.Y = (int)PaddleTwoPos.Y;

                    }

                    //Ai controlled paddle goes down
                    if (BallPos.Y < PaddleTwoPos.Y)
                    {
                        PaddleTwoPos.Y = PaddleTwoPos.Y - 10;
                        PaddleTwoRect.Y = (int)PaddleTwoPos.Y;
                    }

                }




            }

            if (gamestate == GameState.PlayerTwoPlaying)
            {
                if (BallPos.X < 500)
                {
                    if (BallPos.Y < PaddleOnePos.Y)
                    {
                        PaddleOnePos.Y = PaddleOnePos.Y - 10;
                        PaddleOneRect.Y = (int)PaddleOnePos.Y;
                    }

                    //Check if the S key is pressed and adjust the left hand Paddles position and Rectangle Down
                    if (BallPos.Y > PaddleOnePos.Y)
                    {
                        PaddleOnePos.Y = PaddleOnePos.Y + 10;
                        PaddleOneRect.Y = (int)PaddleOnePos.Y;
                    }


                }
            }

            if (gamestate == GameState.Observe)
            {
                //For player Two paddle
                if (BallPos.X > 500)
                {


                    if (BallPos.Y > PaddleTwoPos.Y)
                    {
                        PaddleTwoPos.Y = PaddleTwoPos.Y + 10;
                        PaddleTwoRect.Y = (int)PaddleTwoPos.Y;

                    }

                    //Ai controlled paddle goes down
                    if (BallPos.Y < PaddleTwoPos.Y)
                    {
                        PaddleTwoPos.Y = PaddleTwoPos.Y - 10;
                        PaddleTwoRect.Y = (int)PaddleTwoPos.Y;
                    }
                }
                //For Player One Paddle
                if (BallPos.X < 500)
                {
                    if (BallPos.Y < PaddleOnePos.Y)
                    {
                        PaddleOnePos.Y = PaddleOnePos.Y - 10;
                        PaddleOneRect.Y = (int)PaddleOnePos.Y;
                    }

                    //Check if the S key is pressed and adjust the left hand Paddles position and Rectangle Down
                    if (BallPos.Y > PaddleOnePos.Y)
                    {
                        PaddleOnePos.Y = PaddleOnePos.Y + 10;
                        PaddleOneRect.Y = (int)PaddleOnePos.Y;
                    }
                }
            }
            //Check if the W key is pressed and adjust the left hand Paddles position and Rectangle Up 
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                PaddleOnePos.Y = PaddleOnePos.Y - 10;
                PaddleOneRect.Y = (int)PaddleOnePos.Y;
            }

            //Check if the S key is pressed and adjust the left hand Paddles position and Rectangle Down
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                PaddleOnePos.Y = PaddleOnePos.Y + 10;
                PaddleOneRect.Y = (int)PaddleOnePos.Y;
            }

            //Keep the left hand Paddle from moving above the playable screen
            if (PaddleOnePos.Y <= 100)
            {
                PaddleOnePos.Y = 100;
            }

            //Keep the left hand Paddle from moving below the playable screen
            if (PaddleOnePos.Y + Paddle.Height >= 600)
            {
                PaddleOnePos.Y = 600 - Paddle.Height;
            }


            //Check if the Up key is pressed and adjust the right hand Paddles position and Rectangle Up
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                PaddleTwoPos.Y = PaddleTwoPos.Y - 10;
                PaddleTwoRect.Y = (int)PaddleTwoPos.Y;
            }

            //Check if the Down key is pressed and adjust the right hand Paddles position and Rectangle Down
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                PaddleTwoPos.Y = PaddleTwoPos.Y + 10;
                PaddleTwoRect.Y = (int)PaddleTwoPos.Y;
            }

            //Keep the right hand Paddle from moving above the playable screen
            if (PaddleTwoPos.Y <= 100)
            {
                PaddleTwoPos.Y = 100;
            }

            //Keep the right hand Paddle from moving below the playable screen
            if (PaddleTwoPos.Y + Paddle.Height >= 600)
            {
                PaddleTwoPos.Y = 600 - Paddle.Height;
            }

            //Update the Ball position
            BallPos += BallSpeed;
            //Update the Ball rectangle
            BallRect = new Rectangle((int)BallPos.X, (int)BallPos.Y,
                                        Ball.Width, Ball.Height);

            //Check if the Ball hits the top of the playable screen and change the Y direction of travel
            if (BallPos.Y <= 100)
            {
                BallSpeed.Y = BallSpeed.Y * -1;
            }

            //Check if the Ball hits the bottom of the screen and change the Y direction of travel
            if (BallPos.Y >= 600 - Ball.Height)
            {
                BallSpeed.Y = BallSpeed.Y * -1;
            }

            //If the ball hits the left hand side, reset the ball to the middle of the map and 
            //make the ball move towards the opposite end of the screen
            if (BallPos.X <= 50)
            {
                //BallPos = new Vector2(300 - (Ball.Width / 2), 75 + 150 - (Ball.Height / 2));

                BallSpeed.X = BallSpeed.X * -1;
            }

            //If the ball hits the right hand side, reset the ball to the middle of the map and 
            //make the ball move towards the opposite end of the screen
            if (BallPos.X >= 950 - Ball.Width)
            {
                // BallPos = new Vector2(300 - (Ball.Width / 2),  75 + 150 - (Ball.Height / 2));

                BallSpeed.X = BallSpeed.X * -1;
            }

            //If the ball hits the left hand Paddle change the X direction of travel
            if (BallRect.Intersects(PaddleOneRect))
            {
                BallSpeed.X = BallSpeed.X * -1;
            }

            //If the ball hits the right hand Paddle change the X dirction of travel
            if (BallRect.Intersects(PaddleTwoRect))
            {
                BallSpeed.X = BallSpeed.X * -1;
            }

            //Now for the goals
            if (BallRect.Intersects(GoalOneRect))
            {
                PlayerTwoScore = PlayerTwoScore + 1;

                BallPos = new Vector2(500 - (Ball.Width / 2),
                                   250 - (Ball.Height / 2));

                BallSpeed.X = BallSpeed.X * -1;
            }

            if (BallRect.Intersects(GoalTwoRect))
            {
                PlayerOneScore = PlayerOneScore + 1;

                BallPos = new Vector2(500 - (Ball.Width / 2),
                                  250 - (Ball.Height / 2));

                BallSpeed.X = BallSpeed.X * -1;
            }





            //wait for mouseclick
            mouseState = Mouse.GetState();
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released)
            {
                MouseClicked(mouseState.X, mouseState.Y);
            }

            previousMouseState = mouseState;




            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here

            //Open the SpriteBatch. Make sure you end it after you've finished Drawing
            spriteBatch.Begin();

            //Menus Drawn here
            if (gamestate == GameState.Logo)
            {
                spriteBatch.Draw(LogoScreen, new Vector2((GraphicsDevice.Viewport.Width / 2) - (LogoScreen.Width / 2),
                    (GraphicsDevice.Viewport.Height / 2) - (LogoScreen.Height / 2)), Color.White);
            }
            if (gamestate == GameState.TitleScreen)
            {
                spriteBatch.Draw(Title, new Vector2((GraphicsDevice.Viewport.Width / 2) - (Title.Width / 2),
                   (GraphicsDevice.Viewport.Height / 2) - (Title.Height / 2)), Color.White);
                spriteBatch.Draw(startButton, StartButtPos, Color.White);
                spriteBatch.Draw(exitButton, ExitButtPos, Color.White);
            }

            if (gamestate == GameState.MenuScreen)
            {
                spriteBatch.Draw(Title, new Vector2((GraphicsDevice.Viewport.Width / 2) - (Title.Width / 2),
                   (GraphicsDevice.Viewport.Height / 2) - (Title.Height / 2)), Color.White);

                spriteBatch.Draw(PlayerOneBut, PlayerOneButPos, Color.Aqua);

                //Draw the second Paddle
                spriteBatch.Draw(PlayerTwoBut, PlayerTwoButPos, Color.MediumVioletRed);
                spriteBatch.Draw(MultiPlayer, MultiplayerPos, Color.White);
                spriteBatch.Draw(Observe, Observepos, Color.White);
                spriteBatch.Draw(exitButton, ExitButtPos, Color.White);
            }



            if (gamestate == GameState.Puased)
            {
                spriteBatch.Draw(ScoreDisplay, new Vector2(330, 0), Color.White);
                spriteBatch.Draw(resumeButton, ResumeButtPos, Color.White);
                spriteBatch.Draw(ReturnMainMenu, ReturnButtPos, Color.White);
            }

            if (gamestate == GameState.Playing)
            {//Fraw ScoreBoard Display
                spriteBatch.Draw(ScoreDisplay, new Vector2(330, 0), Color.White);
                spriteBatch.DrawString(ScoreBoard, "Player One", new Vector2(340, 10), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, PlayerOneScore.ToString(), new Vector2(400, 80), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, "Player Two", new Vector2(580, 10), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, PlayerTwoScore.ToString(), new Vector2(650, 80), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, "TIME", new Vector2(510, 30), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, timer.ToString("0.00"), new Vector2(510, 50), Color.Yellow);
                spriteBatch.Draw(PauseButton, PuasedPos, Color.White);
                //First Draw the Background
                spriteBatch.Draw(Background, new Vector2(50, 100), Color.White);

                //Draw the first Paddle
                spriteBatch.Draw(Paddle, PaddleOnePos, Color.Blue);

                //Draw the second Paddle
                spriteBatch.Draw(Paddle, PaddleTwoPos, Color.Red);

                //Draw the Ball
                spriteBatch.Draw(Ball, BallPos, Color.White);

                //Draw goals
                spriteBatch.Draw(Goal, GoalOnePos, Color.Blue);
                spriteBatch.Draw(Goal, GoalTwoPos, Color.Red);
            }
            if (gamestate == GameState.PlayerONePlaying)
            {//Fraw ScoreBoard Display
                spriteBatch.Draw(ScoreDisplay, new Vector2(330, 0), Color.White);
                spriteBatch.DrawString(ScoreBoard, "Player One", new Vector2(340, 10), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, PlayerOneScore.ToString(), new Vector2(400, 80), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, "Player Two", new Vector2(580, 10), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, PlayerTwoScore.ToString(), new Vector2(650, 80), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, "TIME", new Vector2(510, 30), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, timer.ToString("0.00"), new Vector2(510, 50), Color.Yellow);
                spriteBatch.Draw(PauseButton, PuasedPos, Color.White);
                //First Draw the Background
                spriteBatch.Draw(Background, new Vector2(50, 100), Color.White);

                //Draw the first Paddle
                spriteBatch.Draw(Paddle, PaddleOnePos, Color.Blue);

                //Draw the second Paddle
                spriteBatch.Draw(Paddle, PaddleTwoPos, Color.Red);

                //Draw the Ball
                spriteBatch.Draw(Ball, BallPos, Color.White);

                //Draw goals
                spriteBatch.Draw(Goal, GoalOnePos, Color.Blue);
                spriteBatch.Draw(Goal, GoalTwoPos, Color.Red);
            }
            if (gamestate == GameState.PlayerTwoPlaying)
            {//Fraw ScoreBoard Display
                spriteBatch.Draw(ScoreDisplay, new Vector2(330, 0), Color.White);
                spriteBatch.DrawString(ScoreBoard, "Player One", new Vector2(340, 10), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, PlayerOneScore.ToString(), new Vector2(400, 80), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, "Player Two", new Vector2(580, 10), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, PlayerTwoScore.ToString(), new Vector2(650, 80), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, "TIME", new Vector2(510, 30), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, timer.ToString("0.00"), new Vector2(510, 50), Color.Yellow);
                spriteBatch.Draw(PauseButton, PuasedPos, Color.White);
                //First Draw the Background
                spriteBatch.Draw(Background, new Vector2(50, 100), Color.White);

                //Draw the first Paddle
                spriteBatch.Draw(Paddle, PaddleOnePos, Color.Blue);

                //Draw the second Paddle
                spriteBatch.Draw(Paddle, PaddleTwoPos, Color.Red);

                //Draw the Ball
                spriteBatch.Draw(Ball, BallPos, Color.White);

                //Draw goals
                spriteBatch.Draw(Goal, GoalOnePos, Color.Blue);
                spriteBatch.Draw(Goal, GoalTwoPos, Color.Red);
            }
            if (gamestate == GameState.Observe)
            {//Fraw ScoreBoard Display
                spriteBatch.Draw(ScoreDisplay, new Vector2(330, 0), Color.White);
                spriteBatch.DrawString(ScoreBoard, "Player One", new Vector2(340, 10), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, PlayerOneScore.ToString(), new Vector2(400, 80), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, "Player Two", new Vector2(580, 10), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, PlayerTwoScore.ToString(), new Vector2(650, 80), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, "TIME", new Vector2(510, 30), Color.Yellow);
                spriteBatch.DrawString(ScoreBoard, timer.ToString("0.00"), new Vector2(510, 50), Color.Yellow);
                spriteBatch.Draw(PauseButton, PuasedPos, Color.White);
                //First Draw the Background
                spriteBatch.Draw(Background, new Vector2(50, 100), Color.White);

                //Draw the first Paddle
                spriteBatch.Draw(Paddle, PaddleOnePos, Color.Blue);

                //Draw the second Paddle
                spriteBatch.Draw(Paddle, PaddleTwoPos, Color.Red);

                //Draw the Ball
                spriteBatch.Draw(Ball, BallPos, Color.White);

                //Draw goals
                spriteBatch.Draw(Goal, GoalOnePos, Color.Blue);
                spriteBatch.Draw(Goal, GoalTwoPos, Color.Red);
            }

            //Ending the SpriteBatch.
            spriteBatch.End();

            base.Draw(gameTime);
        }


        void MouseClicked(int x, int y)
        {
            Rectangle mouseClickRect = new Rectangle(x, y, 10, 10);

            if (gamestate == GameState.TitleScreen)
            {
                StartRect = new Rectangle((int)StartButtPos.X, (int)StartButtPos.Y, 70, 70);
                ExitRect = new Rectangle((int)ExitButtPos.X, (int)ExitButtPos.Y, 70, 70);
                if (mouseClickRect.Intersects(StartRect))
                {
                    gamestate = GameState.MenuScreen;
                }
                if (mouseClickRect.Intersects(ExitRect))
                {
                    Quit();
                }
            }
            if (gamestate == GameState.MenuScreen)
            {
                ExitRect = new Rectangle((int)ExitButtPos.X, (int)ExitButtPos.Y, 70, 70);
                PlayerOneButRec = new Rectangle((int)PlayerOneButPos.X, (int)PlayerOneButPos.Y, 70, 70);
                PlayerTwoButRect = new Rectangle((int)PlayerTwoButPos.X, (int)PlayerTwoButPos.Y, 70, 70);
                MultiplayerRect = new Rectangle((int)MultiplayerPos.X, (int)MultiplayerPos.Y, 70, 70);
                ObserveRect = new Rectangle((int)Observepos.X, (int)Observepos.Y, 70, 70);

                if (mouseClickRect.Intersects(PlayerOneButRec))
                {
                    gamestate = GameState.PlayerONePlaying;
                    ResetScore();
                }
                if (mouseClickRect.Intersects(PlayerTwoButRect))
                {
                    gamestate = GameState.PlayerTwoPlaying;
                    ResetScore();
                }
                if (mouseClickRect.Intersects(MultiplayerRect))
                {
                    gamestate = GameState.Playing;
                    ResetScore();
                }
                if (mouseClickRect.Intersects(ObserveRect))
                {
                    gamestate = GameState.Observe;
                    ResetScore();
                }
                if (mouseClickRect.Intersects(ExitRect))
                {
                    Quit();
                }
            }
            if (gamestate == GameState.PlayerONePlaying)
            {
                PuasedRect = new Rectangle((int)PuasedPos.X, (int)PuasedPos.Y, 70, 70);

                if (mouseClickRect.Intersects(PuasedRect))
                {
                    gamestate = GameState.Puased;
                }
            }
            if (gamestate == GameState.PlayerTwoPlaying)
            {
                PuasedRect = new Rectangle((int)PuasedPos.X, (int)PuasedPos.Y, 70, 70);

                if (mouseClickRect.Intersects(PuasedRect))
                {
                    gamestate = GameState.Puased;
                }
            }
            if (gamestate == GameState.Playing)
            {
                PuasedRect = new Rectangle((int)PuasedPos.X, (int)PuasedPos.Y, 70, 70);

                if (mouseClickRect.Intersects(PuasedRect))
                {
                    gamestate = GameState.Puased;
                }
            }
            if (gamestate == GameState.Observe)
            {
                PuasedRect = new Rectangle((int)PuasedPos.X, (int)PuasedPos.Y, 70, 70);

                if (mouseClickRect.Intersects(PuasedRect))
                {
                    gamestate = GameState.Puased;
                }
            }
            if (gamestate == GameState.Puased)
            {
                ResumeRect = new Rectangle((int)ResumeButtPos.X, (int)ResumeButtPos.Y, 70, 70);
                ReturnRect = new Rectangle((int)ReturnButtPos.X, (int)ReturnButtPos.Y, 70, 70);

                if (mouseClickRect.Intersects(ResumeRect))
                {
                    if (gamestate == GameState.PlayerONePlaying)
                        gamestate = GameState.PlayerONePlaying;
                    if (gamestate == GameState.PlayerTwoPlaying)
                        gamestate = GameState.PlayerTwoPlaying;
                    if (gamestate == GameState.Playing)
                        gamestate = GameState.Playing;
                    if (gamestate == GameState.Observe)
                        gamestate = GameState.Observe;
                }
                if (mouseClickRect.Intersects(ReturnRect))
                {
                    gamestate = GameState.MenuScreen;
                }
            }
        }


        void TouchObject(int x, int y)
        {
            Rectangle TouchRect = new Rectangle(x, y, 70, 70);
        }



        public void ResetScore()
        {
            PlayerOneScore = 0;
            PlayerTwoScore = 0;
            timer = 300;
        }



        public void Quit()
        {
            this.Exit();
        }
    }
}
