using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calculadora_netframework
{
    public partial class MainWindow : Window
    {
        private double valorAtual = 0;
        private string operacaoPendente = "";
        private bool novoNumero = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void AdicionarAoVisor(string texto)
        {
            if (novoNumero)
            {
                txtVisor.Text = texto;
                novoNumero = false;
            }
            else
            {
                txtVisor.Text += texto;
            }
        }

        // Botão Elevado ao Quadrado
        private void btnQuadrado_Click(object sender, RoutedEventArgs e)
        {
            double numero = double.Parse(txtVisor.Text);
            txtVisor.Text = Math.Pow(numero, 2).ToString(CultureInfo.InvariantCulture);
            novoNumero = true;
        }

        // Botão Mod
        private void btnMod_Click(object sender, RoutedEventArgs e)
        {
            double novoValor = double.Parse(txtVisor.Text);
            valorAtual %= novoValor;
            txtVisor.Text = valorAtual.ToString(CultureInfo.InvariantCulture);
            novoNumero = true;
        }

        // Botão Porcentagem
        private void btnPorcentagem_Click(object sender, RoutedEventArgs e)
        {
            double novoValor = double.Parse(txtVisor.Text);
            txtVisor.Text = (valorAtual * (novoValor / 100)).ToString(CultureInfo.InvariantCulture);
            novoNumero = true;
        }

        // Botão 1/x
        private void btnInverso_Click(object sender, RoutedEventArgs e)
        {
            double numero = double.Parse(txtVisor.Text);
            if (numero != 0)
            {
                txtVisor.Text = (1 / numero).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                MessageBox.Show("Não é possível calcular 1/x para zero.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            novoNumero = true;
        }

        private void btnNumero_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            AdicionarAoVisor(btn.Content.ToString());
        }

        private void btnOperacao_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Calcular();
            operacaoPendente = btn.Content.ToString();
            novoNumero = true;
        }

        private void btnIgual_Click(object sender, RoutedEventArgs e)
        {
            Calcular();
            operacaoPendente = "";
        }

        private void Calcular()
        {
            double novoValor;

            var culture = CultureInfo.InvariantCulture;

            if (double.TryParse(txtVisor.Text, NumberStyles.Float, culture, out novoValor))
            {
                switch (operacaoPendente)
                {
                    case "+":
                        valorAtual += novoValor;
                        break;
                    case "-":
                        valorAtual -= novoValor;
                        break;
                    case "*":
                        valorAtual *= novoValor;
                        break;
                    case "/":
                        if (novoValor != 0)
                        {
                            valorAtual /= novoValor;
                        }
                        else
                        {
                            MessageBox.Show("Não é possível dividir por zero.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    default:
                        valorAtual = novoValor;
                        break;
                }

                txtVisor.Text = valorAtual.ToString(culture);
                novoNumero = true;
            }
            else
            {
                MessageBox.Show("Número inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCE_Click(object sender, RoutedEventArgs e)
        {
            txtVisor.Text = "0";
            novoNumero = true;
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            valorAtual = 0;
            operacaoPendente = "";
            txtVisor.Text = "0";
            novoNumero = true;
        }

        private void btnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (txtVisor.Text.Length > 1)
            {
                txtVisor.Text = txtVisor.Text.Substring(0, txtVisor.Text.Length - 1);
            }
            else
            {
                txtVisor.Text = "0";
                novoNumero = true;
            }
        }

        private void btnRaizQuadrada_Click(object sender, RoutedEventArgs e)
        {
            double numero;

            var culture = CultureInfo.InvariantCulture;

            if (double.TryParse(txtVisor.Text, NumberStyles.Float, culture, out numero))
            {
                if (numero >= 0)
                {
                    txtVisor.Text = Math.Sqrt(numero).ToString(culture); // Formatando o número de acordo
                }
                else
                {
                    MessageBox.Show("Não é possível calcular a raiz quadrada de um número negativo.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                novoNumero = true;
            }
            else
            {
                MessageBox.Show("Número inválido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}