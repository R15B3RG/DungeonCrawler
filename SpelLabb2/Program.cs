using SpelLabb2;
using SpelLabb2.Dice;
using SpelLabb2.GameObjects.Enemies;
using SpelLabb2.GameObjects.Enemies.Level;
using SpelLabb2.GameObjects.Player;
using SpelLabb2.LevelElement;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata;


class Program

{
    static void Main(string[] args)
    {

        Game game = new Game();
        game.Run();
    }
    public class Game
    {
        private string battleText = string.Empty;
        private LevelData levelData = new LevelData();
        private bool _isRunning = true;

        public void Run()
        {
            levelData.Load("Level1.txt");

            while (_isRunning)
            {
                Draw();
                PlayerInput();
                Update();

            }
        }

        public void Draw()
        {
            Console.Clear();

            PlayerStats();

            Console.WriteLine(battleText);

            battleText = string.Empty;
            

            foreach (var element in levelData.Elements)
            {
                Console.SetCursorPosition(0, levelData.Elements.Max(e => e.Y) - 1);
                double distance = Math.Sqrt(Math.Pow(levelData.player.X - element.X, 2) + Math.Pow(levelData.player.Y - element.Y, 2));

                if (element is Enemy enemy && !enemy.ShouldDraw)
                {
                    levelData.RemoveEnemy(enemy);
                    continue; // Hoppa över döda fiender
                }

                if (distance <= 5)
                {
                    element.Draw();

                    if (element is Wall wall)
                    {
                        wall.IsDiscovered = true;

                    }

                }
                else if (element is Wall wall && wall.IsDiscovered)
                {
                    wall.Draw();
                }
            }


        }

        

    public void PlayerInput()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape)
            {
                _isRunning = false;
                return;
            }
            levelData.player.Inputs(key, levelData);
        }

        public void Update()
        {

            var elementsCopy = new List<LevelElement>(levelData.Elements);
       
            foreach (var element in elementsCopy) 
            {
                if (element is Enemy enemy)
                {
                    enemy.Update(levelData.player, levelData);

                    if (Math.Abs(levelData.player.X - enemy.X) + Math.Abs(levelData.player.Y - enemy.Y) == 1)
                    {
                        // Initiera strid mellan spelaren och fienden om de är nära varandra
                        BattleEngine(levelData.player, enemy);

                        if(!enemy.IsAlive)
                        {
                            levelData.RemoveEnemy(enemy);
                        }

                    }
                }
            }
            
        }

        public void PlayerStats()
        {

            Console.Write($"Name: {levelData.player.Name} ||| ");
            Console.Write($"Health: {levelData.player.Health} ||| ");
            Console.Write($"Moves: {levelData.player.Moves} ||| ");
            Console.WriteLine("\n");
        }

        public void BattleEngine(Player player, Enemy enemy)
        {
            string battleOutcome = ""; // Sträng för att lagra hela stridsresultatet

            // Kolla vem som attackerar först baserat på vem som går in i vem
            if (Math.Abs(levelData.player.X - enemy.X) + Math.Abs(levelData.player.Y - enemy.Y) == 1)
            {
                // Kontrollerar vilken karaktär som går in i den andra
                if (levelData.player.X == enemy.X && levelData.player.Y == enemy.Y - 1) // Spelaren går ner
                {
                    // Spelarens attack
                    int playerAttackPoints = player.AttackDice.Throw();
                    int enemyDefencePoints = enemy.DefenceDice.Throw();
                    int damageToEnemy = playerAttackPoints - enemyDefencePoints;

                    // Skriv ut spelarens attack
                    battleOutcome += $"You (ATK: {player.AttackDice} => {playerAttackPoints}) attacked the {enemy.Name} (DEF: {enemy.DefenceDice} => {enemyDefencePoints}), {DescribeEnemyDamage(damageToEnemy)}\n";

                    if (damageToEnemy > 0)
                    {
                        enemy.HP -= damageToEnemy;
                    }

                    // Kontrollera om fienden dog
                    if (enemy.HP <= 0)
                    {
                        battleOutcome += $"The {enemy.Name} has been slain!\n";
                        enemy.ShouldDraw = false;
                        levelData.RemoveEnemy(enemy);
                        battleText = battleOutcome; // Spara stridsresultatet
                        return;
                    }

                    // Fiendens motattack
                    int enemyAttackPoints = enemy.AttackDice.Throw();
                    int playerDefencePoints = player.DefenceDice.Throw();
                    int damageToPlayer = enemyAttackPoints - playerDefencePoints;

                    // Skriv ut fiendens motattack
                    battleOutcome += $"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttackPoints}) attacked you (DEF: {player.DefenceDice} => {playerDefencePoints}), {DescribePlayerDamage(damageToPlayer)}\n";

                    if (damageToPlayer > 0)
                    {
                        player.Health -= damageToPlayer;
                    }
                }
                else // Fienden attackerar först
                {
                    // Fiendens attack
                    int enemyAttackPoints = enemy.AttackDice.Throw();
                    int playerDefencePoints = player.DefenceDice.Throw();
                    int damageToPlayer = enemyAttackPoints - playerDefencePoints;

                    // Skriv ut fiendens attack
                    battleOutcome += $"The {enemy.Name} (ATK: {enemy.AttackDice} => {enemyAttackPoints}) attacked you (DEF: {player.DefenceDice} => {playerDefencePoints}), {DescribePlayerDamage(damageToPlayer)}\n";

                    if (damageToPlayer > 0)
                    {
                        player.Health -= damageToPlayer;
                    }

                    // Kontrollera om spelaren dog
                    if (player.Health <= 0)
                    {
                        battleOutcome += "You have been defeated! Game Over.\n";
                        Environment.Exit(0); // Avsluta spelet
                        return; // Ingen mer loggning behövs
                    }

                    // Spelarens motattack
                    int playerAttackPoints = player.AttackDice.Throw();
                    int enemyDefencePoints = enemy.DefenceDice.Throw();
                    int damageToEnemy = playerAttackPoints - enemyDefencePoints;

                    // Skriv ut spelarens attack
                    battleOutcome += $"You (ATK: {player.AttackDice} => {playerAttackPoints}) attacked the {enemy.Name} (DEF: {enemy.DefenceDice} => {enemyDefencePoints}), {DescribeEnemyDamage(damageToEnemy)}\n";

                    if (damageToEnemy > 0)
                    {
                        enemy.HP -= damageToEnemy;
                    }

                    // Kontrollera om fienden dog
                    if (enemy.HP <= 0)
                    {
                        battleOutcome += $"The {enemy.Name} has been slain!\n";
                        enemy.ShouldDraw = false;
                        levelData.RemoveEnemy(enemy);
                    }
                }
            }

            battleText = battleOutcome; // Spara stridsresultatet
        }



        // Beskrivning av skada beroende på differens
        private string DescribeEnemyDamage(int damageToEnemy)
        {
            if (damageToEnemy > 10)
            {
                return "severely wounding it.";
            }
            else if (damageToEnemy > 5)
            {
                return "moderately wounding it.";
            }
            else if (damageToEnemy > 0)
            {
                return "slightly wounding it.";
            }
            else
            {
                return "but it was not effective.";
            }
        }

        private string DescribePlayerDamage(int damageToPlayer)
        {
            if (damageToPlayer > 10)
            {
                return "severely wounding you.";
            }
            else if (damageToPlayer > 5)
            {
                return "moderately wounding you.";
            }
            else if (damageToPlayer > 0)
            {
                return "slightly wounding you.";
            }
            else
            {
                return "but it was not effective.";
            }
        }


    }
}





