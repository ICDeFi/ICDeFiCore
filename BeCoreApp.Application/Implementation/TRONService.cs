using BeCoreApp.Application.Interfaces;
using BeCoreApp.Application.ViewModels.TronBlockChain;
using BeCoreApp.Utilities.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BeCoreApp.Application.Implementation
{
    public class TRONService : ITRONService
    {
        private readonly IHttpService _httpService;
        public TRONService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<BaseResponse> EasyTransferAssetByPrivate(string privateKey, string toAddress, string assetAddress, BigInteger amount)
        {
            BaseResponse baseResponse = null;

            var url = $"{CommonConstants.TRONUrl}easytransfertrc20byprivate";

            var EasyTransferAssetByPrivateRequest = new EasyTransferAssetByPrivateRequest
            {
                privateKey = privateKey,
                toAddress = toAddress,
                amount = amount,
                assetAddress = assetAddress,
            };

            var data = await _httpService.PostAsync(url, EasyTransferAssetByPrivateRequest);

            if (data.Success == true)
                baseResponse = JsonConvert.DeserializeObject<BaseResponse>(data.Message);

            return baseResponse;
        }

        public async Task<BaseResponse> EasyTransferByPrivate(string privateKey, string toAddress, BigInteger amount)
        {
            BaseResponse baseResponse = null;

            var url = $"{CommonConstants.TRONUrl}easytransferbyprivate";

            var EasyTransferByPrivateRequest = new EasyTransferByPrivateRequest
            {
                amount = amount,
                privateKey = privateKey,
                toAddress = toAddress,
            };

            var data = await _httpService.PostAsync(url, EasyTransferByPrivateRequest);

            if (data.Success == true)
                baseResponse = JsonConvert.DeserializeObject<BaseResponse>(data.Message);

            return baseResponse;
        }

        public async Task<Generateaddress> GenerateAddress()
        {
            Generateaddress generateaddress = null;

            var url = $"{CommonConstants.TRONUrl}generateaddress";

            var data = await _httpService.GetAsync(url);

            if (data.Success == true)
                generateaddress = JsonConvert.DeserializeObject<Generateaddress>(data.Message);

            return generateaddress;
        }

        public async Task<BaseResponse> GetTRC20BalanceByAddress(string address, string assetAddress)
        {
            BaseResponse baseResponse = null;

            var url = $"{CommonConstants.TRONUrl}gettrc20balancebyaddress?address={address}&assetAddress={assetAddress}";

            var data = await _httpService.GetAsync(url);

            if (data.Success == true)
                baseResponse = JsonConvert.DeserializeObject<BaseResponse>(data.Message);

            return baseResponse;
        }

        public async Task<BaseResponse> GetBalanceByAddress(string address)
        {
            BaseResponse baseResponse = null;

            var url = $"{CommonConstants.TRONUrl}getbalancebyaddress?address={address}";

            var data = await _httpService.GetAsync(url);

            if (data.Success == true)
                baseResponse = JsonConvert.DeserializeObject<BaseResponse>(data.Message);

            return baseResponse;
        }

        public async Task<BaseResponse> ValidateAddress(string address)
        {
            BaseResponse baseResponse = null;

            var url = $"{CommonConstants.TRONUrl}validateaddress?address={address}";

            var data = await _httpService.GetAsync(url);

            if (data.Success == true)
                baseResponse = JsonConvert.DeserializeObject<BaseResponse>(data.Message);

            return baseResponse;
        }
    }
}
