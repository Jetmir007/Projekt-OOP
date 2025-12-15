using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projekt_OOP;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private GameState _current = GameState.MainMenu;

    private SpriteFont _font;
    private Texture2D _boxer, _swordsman, _ninja;

    private CharacterBase player1, player2;
    private PlayerController controller1, controller2;

    private CharacterType _p1Choice = CharacterType.None;
    private CharacterType _p2Choice = CharacterType.None;
    private CharacterType _winner = CharacterType.None;

    private KeyboardState _previousState;

    private readonly Vector2 player1StartPos = new Vector2(100, 300);
    private readonly Vector2 player2StartPos = new Vector2(600, 300);

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferWidth = 1800;
        _graphics.PreferredBackBufferHeight = 1000;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _font = Content.Load<SpriteFont>("font");
        _boxer = Content.Load<Texture2D>("boxer(1)");
        _swordsman = Content.Load<Texture2D>("sword");
        _ninja = Content.Load<Texture2D>("ninja");

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        var kState = Keyboard.GetState();
        switch (_current)
        {
            case GameState.MainMenu:
                MainMenu(kState);
                break;

            case GameState.CharacterSelect:
                CharacterSelect(kState);
                break;

            case GameState.InGame:
                InGame(kState, gameTime);
                break;
            
            case GameState.Results:
                GameOver(kState);
                break;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here

        _spriteBatch.Begin();

        switch (_current)
        {
            case GameState.MainMenu:
                DrawMainMenu();
                break;
            case GameState.CharacterSelect:
                DrawCharacterSelect();
                break;
            case GameState.InGame:
                DrawInGame();
                break;
            case GameState.Results:
                _spriteBatch.DrawString(_font, $"{_winner} Wins!", new Vector2(800, 450), Color.White);
                _spriteBatch.DrawString(_font, "Press Enter to Restart", new Vector2(750, 500), Color.White);
                break;
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void MainMenu(KeyboardState kState)
    {
        if (kState.IsKeyDown(Keys.Enter) && !_previousState.IsKeyDown(Keys.Enter))
        {
            _current = GameState.CharacterSelect;
        }

        _previousState = kState;
    }

    private void CharacterSelect(KeyboardState kState)
    {
        if(_p1Choice == CharacterType.None)
        {
            if (kState.IsKeyDown(Keys.A) && _previousState.IsKeyUp(Keys.A))
            {
                _p1Choice = GetCharacter(_p1Choice == CharacterType.None ? CharacterType.Ninja : _p1Choice);
            }
            if (kState.IsKeyDown(Keys.D) && _previousState.IsKeyUp(Keys.D))
            {
                _p1Choice = GetCharacter(_p1Choice == CharacterType.None ? CharacterType.Ninja : _p1Choice);
            }
        }

        else if(_p2Choice == CharacterType.None)
        {
            if (kState.IsKeyDown(Keys.Left) && _previousState.IsKeyUp(Keys.Left))
            {
                _p2Choice = GetCharacter(_p2Choice == CharacterType.None ? CharacterType.Ninja : _p2Choice);
            }
            if (kState.IsKeyDown(Keys.Right) && _previousState.IsKeyUp(Keys.Right))
            {
                _p2Choice = GetCharacter(_p2Choice == CharacterType.None ? CharacterType.Ninja : _p2Choice);
            }
        }

        if(_p1Choice != CharacterType.None && _p2Choice != CharacterType.None && kState.IsKeyDown(Keys.Enter) && !_previousState.IsKeyDown(Keys.Enter))
        {
            player1 = CreateCharacter(_p1Choice, player1StartPos);
            player2 = CreateCharacter(_p2Choice, player2StartPos);

            controller1 = new PlayerController(player1, player2, Keys.W, Keys.S, Keys.A, Keys.D, Keys.F, Keys.G);
            controller2 = new PlayerController(player2, player1, Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.NumPad1, Keys.NumPad2);

            _current = GameState.InGame;
        }
    }

    private CharacterBase CreateCharacter(CharacterType type, Vector2 startPos)
    {
        return type switch
        {
            CharacterType.Boxer => new Boxer(_boxer, startPos),
            CharacterType.Swordsman => new Swordsman(_swordsman, startPos),
            CharacterType.Ninja => new Ninja(_ninja, startPos),
            _ => null,
        };
    }

    private CharacterType GetCharacter(CharacterType current)
    {
        return current switch
        {
            CharacterType.Boxer => CharacterType.Swordsman,
            CharacterType.Swordsman => CharacterType.Ninja,
            CharacterType.Ninja => CharacterType.Boxer,
            _ => CharacterType.Boxer
        };
    }

    private void InGame(KeyboardState kState, GameTime gametime)
    {
        controller1.HandleInput(kState, _previousState);
        controller2.HandleInput(kState, _previousState);

        player1.Update(gametime);
        player2.Update(gametime);

        if(player1.Hp <= 0)
        {
            _winner = _p2Choice;
            _current = GameState.Results;
        }
        else if(player2.Hp <= 0)
        {
            _winner = _p1Choice;
            _current = GameState.Results;
        }
    }

    private void GameOver(KeyboardState kState)
    {
        if (kState.IsKeyDown(Keys.Enter) && !_previousState.IsKeyDown(Keys.Enter))
        {
            _p1Choice = CharacterType.None;
            _p2Choice = CharacterType.None;
            _winner = CharacterType.None;

            _current = GameState.CharacterSelect;
        }

        _previousState = kState;
    }

    private void DrawMainMenu()
    {
        _spriteBatch.DrawString(_font, "Press Enter to Start", new Vector2(800, 450), Color.White);
        _spriteBatch.DrawString(_font, "Fighting Game", new Vector2(800, 150), Color.White);
    }

    private void DrawCharacterSelect()
    {
        _spriteBatch.DrawString(_font, "Choose Your Character", new Vector2(750, 100), Color.White);
        CharacterType p1t = _p1Choice == CharacterType.None ? CharacterType.Boxer : _p1Choice;
        Texture2D p1text = TextureType(p1t);
        _spriteBatch.Draw(p1text, new Vector2(400, 300), Color.White);

        CharacterType p2t = _p2Choice == CharacterType.None ? CharacterType.Boxer : _p2Choice;
        Texture2D p2text = TextureType(p2t);
        _spriteBatch.Draw(p2text, new Vector2(1200, 300), Color.White);
    }

    private void DrawInGame()
    {
        player1.Draw(_spriteBatch);
        player2.Draw(_spriteBatch);
    }

    private Texture2D TextureType(CharacterType type)
    {
        return type switch
        {
            CharacterType.Boxer => _boxer,
            CharacterType.Swordsman => _swordsman,
            CharacterType.Ninja => _ninja,
            _ => null,
        };
    }
}