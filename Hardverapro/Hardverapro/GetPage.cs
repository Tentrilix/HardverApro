﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms.VisualStyles;
using System.Collections;
using System.Windows.Forms;

namespace Hardverapro {
    class GetPage : IGetPage {
        private HttpClient httpClient;

        public GetPage(int nor, List<string> currads) {
            numberofresults = nor;
            currentads = currads;
        }

        public int numberofresults { get; set; }
        public List<String> currentads { get; set; }

        void IGetPage.GetPage() {
            httpClient = new HttpClient();
        }

        List<string> IGetPage.RunTask() {
            //return Task.FromResult<List<string>>()
            throw new NotImplementedException("Soon™");
        }

        async Task<List<string>> IGetPage.Main() {
            try {
                HttpResponseMessage response = await httpClient.GetAsync("https://hardverapro.hu/aprok/hardver/videokartya/keres.php?stext=1660&county=&stcid=&settlement=&stmid=&minprice=&maxprice=&company=&cmpid=&user=&usrid=&selling=1&stext_none=");
                response.EnsureSuccessStatusCode();
                string responsebody = await response.Content.ReadAsStringAsync();
                return responsebody.GetAds();
            } catch (HttpRequestException e) {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<string>();
            }
        }
    }
}
