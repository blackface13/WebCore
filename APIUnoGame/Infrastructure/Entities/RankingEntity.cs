using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIUnoGame.Infrastructure.Entities
{
    public class Ranking
    {
        [Key]
        public string UserID{get;set;}
        public string UserName{get;set;}
        public string Country{get;set;}
        public Int64 UnoBasicWinRound{get;set;}
        public Int64 UnoBasicLoseRound {get;set;}
        public Int64 UnoBasicPoint {get;set;}
        public Int64 UnoExtensionWinRound {get;set;}
        public Int64 UnoExtensionLoseRound {get;set;}
        public Int64 UnoExtensionPoint {get;set;}
    }
}
