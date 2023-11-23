// See https://aka.ms/new-console-template for more information
using DataNoSQL;
using DataNoSQL.DAL;
using DataNoSQL.DTC;

internal partial class Program
{
    private enum TipoCarregador { AC = 0, DC = 1 };
    private static List<string> Localizacoes;

    private static void Main(string[] args)
    {   
        Console.WriteLine("Este programa é usado para gerar registros dos pontos de recarga!");

        Localizacoes = new List<string>();
        Localizacoes.Add("-23.6043094, -46.6818203");
        Localizacoes.Add("-23.5982635, -46.6918105,17");
        Localizacoes.Add("-23.5985184, -46.6813641,17");
        Localizacoes.Add("-23.595735, -46.6728676,17");
        Localizacoes.Add("-23.6025586, -46.6857085,17");

        for (int i = 0; i < 5; i++)
            GeraPontosRecarga(i);

        Console.WriteLine("\nPontos de recarga gerados");
        Thread.Sleep(500);

        for (int i = 0; i < 100; i++)
            GeraHistorico();

        Console.WriteLine("\nLog dos pontos de recarga gerado");

    }
    private static void GeraPontosRecarga(int posicaoLocalizacao)
    {

        var random = new Random();
        var pontoRecargaDal = new PontoRecargaDALNoSQL();
        var empresaDal = new EmpresaDALNoSQL();
        var empresas = empresaDal.read();
        var listaEmpresas = new List<Guid>();
        foreach (var empresa in empresas)
            listaEmpresas.Add(empresa.Id);

        pontoRecargaDal.Insert(new PontoRecargaDTCNoSQL
        {
            Id = Guid.NewGuid(),
            DataInclusao = DateTime.Now,
            EmpresaId = listaEmpresas.ElementAt(random.Next(listaEmpresas.Count)),
            Localizacao = Localizacoes.ElementAt(posicaoLocalizacao),
            TipoCarregador = ObterValorAleatorio<TipoCarregador>().ToString()
        });

    }
    private static void GeraHistorico()
    {
        var random = new Random();
        var historicoPontoRecargaDal = new HistoricoPontoRecargaDALNoSQL();
        var pontoRecargaDal = new PontoRecargaDALNoSQL();
        var pontosRecarga = pontoRecargaDal.read();
        var listaPontos = new List<Guid>();
        foreach (var pontoRecarga in pontosRecarga)
            listaPontos.Add(pontoRecarga.Id);

        DateTime dataInicio = new DateTime(2023, 1, 1, 0, 0, 0);
        int rangeDays = (DateTime.Today - dataInicio).Days;
        DateTime randomDate = dataInicio.AddDays(random.Next(rangeDays));
        randomDate = randomDate.AddHours(random.Next(25));
        randomDate = randomDate.AddMinutes(random.Next(61));
        randomDate = randomDate.AddSeconds(random.Next(61));
        randomDate = randomDate.AddMilliseconds(random.Next(1000));

        historicoPontoRecargaDal.Insert(new HistoricoPontoRecargaDTCNoSQL
        {
            Id = Guid.NewGuid(),
            DataHora = randomDate,
            Disponivel = random.Next(2) == 0,
            PontoRecargaId = listaPontos.ElementAt(random.Next(listaPontos.Count)),
        });

    }

    private static T ObterValorAleatorio<T>() where T : Enum
    {
        var valores = Enum.GetValues(typeof(T));
        var r = new Random();
        return (T)valores.GetValue(r.Next(valores.Length));
    }

}