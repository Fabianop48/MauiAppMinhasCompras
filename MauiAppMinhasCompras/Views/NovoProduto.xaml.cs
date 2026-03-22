using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Verificação básica para evitar erro de conversão antes mesmo de chegar no Model
            if (string.IsNullOrWhiteSpace(txt_quantidade.Text) || string.IsNullOrWhiteSpace(txt_preco.Text))
            {
                await DisplayAlert("Ops", "Preencha a quantidade e o preço corretamente.", "OK");
                return;
            }

            // Criando o objeto Produto
            // As validações de "maior que zero" e "descrição obrigatória" 
            // que estão no Model serão disparadas aqui nas atribuições.
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Insere no banco de dados SQLite
            await App.Db.Insert(p);

            await DisplayAlert("Sucesso!", "Produto salvo com sucesso!", "OK");

            // Fecha a tela atual e volta para a Lista de Produtos
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            // Captura as exceções lançadas pelo Model (ex: valor <= 0)
            // ou erros inesperados e exibe a mensagem amigável.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}