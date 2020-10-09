using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tic_Tac_Toe___with_AI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer tmrTick;
        public MainWindow()
        {
            InitializeComponent();

            tmrTick = new DispatcherTimer();
            tmrTick.Interval = TimeSpan.FromSeconds(0.25);
            tmrTick.Tick += tmrTick_Tick;
        }

        BlurEffect b = new BlurEffect();

        public string difficulty = "";

        public const int inf = (1 << 20), aiWinScore = 100, tieScore = 0;
        public const double firstRate = 60;

        public char human = 'X', ai = 'O', tie = '-', spatiu = ' ';

        public char toFill = ' ';
       
        public List<string> movesLeft = new List<string>();

        Dictionary<char, int> scores = new Dictionary<char, int>();

        public bool playerTurn = false;
        public char[,] board = new char[3, 3];

        public int nextI, nextJ;

        public bool timerIsOn = false;

        private void tmrTick_Tick(object sender, EventArgs e)
        {
            if (nextI == 3)
            {
                tmrTick.Stop();

                string s = "";

                if (toFill == ai)
                    s = "lose";
                else
                    s = "win";

                CustomMessageBox cmb = new CustomMessageBox(s);
                cmb.Owner = this;

                b.Radius = 20;
                this.Effect = b;

                if (cmb.ShowDialog() == true)
                {     
                    InitializeGame();           
                }
                else
                {
                    GameGrid.Visibility = Visibility.Hidden;
                    StartGame.Visibility = Visibility.Visible;
                }

                b.Radius = 0;
                return;
            }    

            (GameGrid.FindName("tb" + nextI.ToString() + nextJ.ToString()) as TextBlock).Text = toFill.ToString();

            if (toFill == ai)
            {
                (GameGrid.FindName("tb" + nextI.ToString() + nextJ.ToString()) as TextBlock).Foreground = Brushes.Red;
            }
            else
            {
                (GameGrid.FindName("tb" + nextI.ToString() + nextJ.ToString()) as TextBlock).Foreground = Brushes.Green;
            }
            

            nextJ++;
            
            if (nextJ == 3)
            {
                nextJ = 0;
                nextI++;
            } 
        }
        public int check()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2] && board[i, 0] != spatiu)
                {
                    if (board[i, 0] == ai)
                        return aiWinScore;
                    else
                        return -aiWinScore;
                }


                if (board[0, i] == board[1, i] && board[0, i] == board[2, i] && board[0, i] != spatiu)
                {
                    if (board[0, i] == ai)
                        return aiWinScore;
                    else
                        return -aiWinScore;
                }
            }

            if (board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] && board[0, 0] != spatiu)
                if (board[0, 0] == ai)
                    return aiWinScore;
                else
                    return -aiWinScore;

            if (board[2, 0] == board[1, 1] && board[2, 0] == board[0, 2] && board[2, 0] != spatiu)
                if (board[2, 0] == ai)
                    return aiWinScore;
                else
                    return -aiWinScore;

            return 0;
        }

        public bool NoMovesLeft()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == spatiu)
                        return false;

            return true;
        }

        public int minimax(int depth, int alpha, int beta, bool isMax = true)
        {
            int value = check();

            if (value != 0)
            {
                return value;
            }

            if (NoMovesLeft())
                return 0;

            if (isMax)
            {
                int best = -inf;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == spatiu)
                        {
                            board[i, j] = ai;

                            int eval = minimax(depth + 1, alpha, beta, !isMax);

                            board[i, j] = spatiu;

                            best = Math.Max(best, eval);

                            alpha = Math.Max(alpha, eval);

                            if (beta <= alpha)
                                break;
                        }
                    }
                }

                return best - depth;
            }
            else
            {
                int best = inf;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == spatiu)
                        {
                            board[i, j] = human;

                            int eval = minimax(depth + 1, alpha, beta, !isMax);

                            board[i, j] = spatiu;

                            best = Math.Min(best, eval);

                            beta = Math.Min(beta, eval);

                            if (beta <= alpha)
                                break;

                        }
                    }
                }

                return best + depth;
            }
        }

        public void findBestMove()
        {
            int best = -inf;
            int bi = -1, bj = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == spatiu)
                    {
                        board[i, j] = ai;

                        int current = minimax(0, -inf, inf, false);

                        board[i, j] = spatiu;

                        if (current > best)
                        {
                            best = current;
                            bi = i;
                            bj = j;
                        }
                    }
                }
            }

            board[bi, bj] = ai;
            (GameGrid.FindName("tb" + bi.ToString() + bj.ToString()) as TextBlock).Text = ai.ToString();
            (GameGrid.FindName("tb" + bi.ToString() + bj.ToString()) as TextBlock).Foreground = Brushes.Red;

            int c = check();

            if (c == aiWinScore)
            {
                CustomMessageBox cmb = new CustomMessageBox("lose");
                cmb.Owner = this;

                b.Radius = 20;
                this.Effect = b;

                if (cmb.ShowDialog() == true)
                {
                    InitializeGame();
                }
                else
                {
                    GameGrid.Visibility = Visibility.Hidden;
                    StartGame.Visibility = Visibility.Visible;
                }

                b.Radius = 0;
            }
            else if (NoMovesLeft())
            {
                CustomMessageBox cmb = new CustomMessageBox("tie");
                cmb.Owner = this;

                b.Radius = 20;
                this.Effect = b;

                if (cmb.ShowDialog() == true)
                {
                    InitializeGame();
                }
                else
                {
                    GameGrid.Visibility = Visibility.Hidden;
                    StartGame.Visibility = Visibility.Visible;
                }

                b.Radius = 0;   
            }

            playerTurn = true;
        }

        private void btnPlayEasy_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size n = e.NewSize;
            Size o = e.PreviousSize;

            double l = n.Width / o.Width;

            if (l != double.PositiveInfinity)
            {
                btnPlayEasy.FontSize *= l;
            }
        }

        private void btnPlayHard_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size n = e.NewSize;
            Size o = e.PreviousSize;

            double l = n.Width / o.Width;

            if (l != double.PositiveInfinity)
            {
                btnPlayHard.FontSize *= l;
            }
        }

        private void btnPlayImpossible_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size n = e.NewSize;
            Size o = e.PreviousSize;

            double l = n.Width / o.Width;

            if (l != double.PositiveInfinity)
            {
                btnPlayImpossible.FontSize *= l;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.End && GameGrid.Visibility == Visibility.Visible && !timerIsOn)
            {
                toFill = human;
                timerIsOn = true;
                tmrTick.Start();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            scores.Add(ai, aiWinScore);
            scores.Add(human, -aiWinScore);
            scores.Add(tie, tieScore);
        }

        public void InitializeGame()
        {
            movesLeft.Clear();
            playerTurn = false;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    (GameGrid.FindName("tb" + i.ToString() + j.ToString()) as TextBlock).Text = "";
                    board[i, j] = spatiu;
                    movesLeft.Add(i.ToString() + j.ToString());
                }

            if (difficulty != "impossible")
            {
                Random a = new Random();

                int rate = a.Next(0, 100);

                if (rate > firstRate)
                {
                    ai = 'X';
                    human = 'O';

                    Random r = new Random();

                    int ri = r.Next(0, 3);
                    int rj = r.Next(0, 3);

                    board[ri, rj] = ai;
                    (GameGrid.FindName("tb" + ri.ToString() + rj.ToString()) as TextBlock).Text = ai.ToString();
                    (GameGrid.FindName("tb" + ri.ToString() + rj.ToString()) as TextBlock).Foreground = Brushes.Red;

                    movesLeft.Remove(ri.ToString() + rj.ToString());
                }
                else
                {
                    ai = 'O';
                    human = 'X';
                }
            }
            else
            {
                human = 'X';
                ai = 'O';
            }

            playerTurn = true;

            nextI = 0;
            nextJ = 0;
            timerIsOn = false;
        }

        private void btnPlayEasy_Click(object sender, RoutedEventArgs e)
        {
            ///Easy - Selects random a valid position

            difficulty = "easy";

            GameGrid.Visibility = Visibility.Visible;
            StartGame.Visibility = Visibility.Hidden;

            InitializeGame();
        }

        private void btnPlayHard_Click(object sender, RoutedEventArgs e)
        {
            /// Medium - minimax algorithm

            difficulty = "medium";

            GameGrid.Visibility = Visibility.Visible;
            StartGame.Visibility = Visibility.Hidden;

            InitializeGame();
        }

        private void btnPlayImpossible_Click(object sender, RoutedEventArgs e)
        {
            /// Impossible

            difficulty = "impossible";

            GameGrid.Visibility = Visibility.Visible;
            StartGame.Visibility = Visibility.Hidden;

            InitializeGame();
        }


        private void lb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (timerIsOn)
                return;

            Label l = sender as Label;

            int indexI = int.Parse(l.Name[2].ToString());
            int indexJ = int.Parse(l.Name[3].ToString());

            TextBlock t = GameGrid.FindName("tb" + indexI.ToString() + indexJ.ToString()) as TextBlock;

            if (difficulty == "medium")
            {
                if (playerTurn && t.Text == "")
                {
                    playerTurn = false;

                    board[indexI, indexJ] = human;

                    t.Text = human.ToString();
                    t.Foreground = Brushes.Green;

                    int c = check();

                    if (c == -aiWinScore)
                    {
                        CustomMessageBox cmb = new CustomMessageBox("win");
                        cmb.Owner = this;

                        b.Radius = 20;
                        this.Effect = b;

                        if (cmb.ShowDialog() == true)
                        {
                            InitializeGame();
                        }
                        else
                        {
                            GameGrid.Visibility = Visibility.Hidden;
                            StartGame.Visibility = Visibility.Visible;
                        }

                        b.Radius = 0;
                    }
                    else if (NoMovesLeft())
                    {
                        CustomMessageBox cmb = new CustomMessageBox("tie");
                        cmb.Owner = this;

                        b.Radius = 20;
                        this.Effect = b;

                        if (cmb.ShowDialog() == true)
                        {
                            InitializeGame();
                        }
                        else
                        {
                            GameGrid.Visibility = Visibility.Hidden;
                            StartGame.Visibility = Visibility.Visible;
                        }

                        b.Radius = 0;
                    }
                    else
                    {
                        findBestMove();
                    }
                }
            }
            else if (difficulty == "easy")
            {
                if (playerTurn && t.Text == "")
                {
                    playerTurn = false;

                    board[indexI, indexJ] = human;

                    movesLeft.Remove(indexI.ToString() + indexJ.ToString());

                    t.Text = human.ToString();
                    t.Foreground = Brushes.Green;

                    int c = check();

                    if (c == -aiWinScore)
                    {
                        CustomMessageBox cmb = new CustomMessageBox("win");
                        cmb.Owner = this;

                        b.Radius = 20;
                        this.Effect = b;

                        if (cmb.ShowDialog() == true)
                        {
                            InitializeGame();
                        }
                        else
                        {
                            GameGrid.Visibility = Visibility.Hidden;
                            StartGame.Visibility = Visibility.Visible;
                        }

                        b.Radius = 0;
                    }
                    else if (NoMovesLeft())
                    {
                        CustomMessageBox cmb = new CustomMessageBox("tie");
                        cmb.Owner = this;

                        b.Radius = 20;
                        this.Effect = b;

                        if (cmb.ShowDialog() == true)
                        {
                            InitializeGame();
                        }
                        else
                        {
                            GameGrid.Visibility = Visibility.Hidden;
                            StartGame.Visibility = Visibility.Visible;
                        }

                        b.Radius = 0;
                    }
                    else
                    {
                        Random rnd = new Random();

                        int moveIndex = rnd.Next(0, movesLeft.Count);

                        if (movesLeft.Count == 0)
                            return;

                        int movei = int.Parse((movesLeft[moveIndex][0]).ToString());
                        int movej = int.Parse((movesLeft[moveIndex][1]).ToString());

                        board[movei, movej] = ai;

                        movesLeft.RemoveAt(moveIndex);

                        (GameGrid.FindName("tb" + movei.ToString() + movej.ToString()) as TextBlock).Text = ai.ToString();
                        (GameGrid.FindName("tb" + movei.ToString() + movej.ToString()) as TextBlock).Foreground = Brushes.Red;

                        c = check();

                        if (c == aiWinScore)
                        {
                            CustomMessageBox cmb = new CustomMessageBox("lose");
                            cmb.Owner = this;

                            b.Radius = 20;
                            this.Effect = b;

                            if (cmb.ShowDialog() == true)
                            {
                                InitializeGame();
                            }
                            else
                            {
                                GameGrid.Visibility = Visibility.Hidden;
                                StartGame.Visibility = Visibility.Visible;
                            }

                            b.Radius = 0;
                        }
                        else if (NoMovesLeft())
                        {
                            CustomMessageBox cmb = new CustomMessageBox("tie");
                            cmb.Owner = this;

                            b.Radius = 20;
                            this.Effect = b;

                            if (cmb.ShowDialog() == true)
                            {
                                InitializeGame();
                            }
                            else
                            {
                                GameGrid.Visibility = Visibility.Hidden;
                                StartGame.Visibility = Visibility.Visible;
                            }

                            b.Radius = 0;
                        }

                        playerTurn = true;
                    }
                }
            }
            else if (difficulty == "impossible")
            {
                if (board[indexI, indexJ] != spatiu)
                    return;

                playerTurn = false;

                t.Text = "X";
                t.Foreground = Brushes.Green;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        board[i, j] = ai;
                    }
                }

                toFill = ai;
                timerIsOn = true;
                tmrTick.Start();
            }



        }
    }
}
