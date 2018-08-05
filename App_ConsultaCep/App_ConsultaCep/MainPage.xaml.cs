using App_ConsultaCep.Servico;
using App_ConsultaCep.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_ConsultaCep
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BOTAO.Clicked += BuscarCEP;
		}

        private void BuscarCEP(object sender, EventArgs e)
        {
            //TODO: Lógica do Programa.
            String cep = CEP.Text.Trim();

            //TODO: Validações.
            if (isCEPValido(cep))
            {
                //TODO: Tratamento das exceções. Tratando cada uma delas individualmente tem maior controle das mensagens.
                try
                {
                    Endereco end = ViaCepServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                        RESULTADO.Text = String.Format("Endereço: {0}, {1} - {2}/{3}", end.logradouro, end.bairro, end.localidade, end.uf);
                    else
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + CEP.Text, "OK");
                }
                catch (Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
                }
            }
            
        }

        //TOD: Verificar para fazer validações utilizando DataAnnotations
        private bool isCEPValido(string cep)
        {
            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres.", "OK");
                return false;
            }

            int novoCep = 0;
            if(!int.TryParse(cep,out novoCep))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto apenas por números.", "OK");
                return false;
            }

            return true;
        }
    }
}
