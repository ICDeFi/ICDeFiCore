using System;
using System.Collections.Generic;
using System.Text;

namespace BeCoreApp.Application.ViewModels.BlockChain
{
    public class CoinMarKetCapInfoViewModel
    {
        public CoinMarKetCapInfoViewModel()
        {
            data = new List<CoinData>();
        }

        public CoinStatus status { get; set; }
        public List<CoinData> data { get; set; }
    }
    public class CoinData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string slug { get; set; }
        public int num_market_pairs { get; set; }
        public DateTime date_added { get; set; }
        public List<string> tags { get; set; }
        public decimal? max_supply { get; set; }
        public decimal circulating_supply { get; set; }
        public decimal total_supply { get; set; }
        public object platform { get; set; }
        public int cmc_rank { get; set; }
        public DateTime last_updated { get; set; }
        public CoinQuote quote { get; set; }

    }
    public class CoinQuote
    {
        public CoinUSD USD { get; set; }
    }

    public class CoinUSD
    {
        public decimal price { get; set; }
        public decimal volume_24h { get; set; }
        public decimal percent_change_1h { get; set; }
        public decimal percent_change_24h { get; set; }
        public decimal percent_change_7d { get; set; }
        public decimal market_cap { get; set; }
        public DateTime last_updated { get; set; }
    }

    public class CoinStatus
    {
        public DateTime timestamp { get; set; }
        public int error_code { get; set; }
        public string error_message { get; set; }
        public int elapsed { get; set; }
        public int credit_count { get; set; }
        public string notice { get; set; }
    }
}
