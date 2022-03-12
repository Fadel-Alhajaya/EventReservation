using Dapper;
using EventReservation.Core.Common;
using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EventReservation.Core.Repository
{
    public class CardRepository: ICardRepository
    {
        private readonly IDbContext DbContext;
        public CardRepository(IDbContext _DbContext)
        {
            DbContext = _DbContext;
        }
        public List<Card> GetAllCards()
        {
            IEnumerable<Card> result = DbContext.Connection.Query<Card>("CARD_F_PACKAGE.GETALLCARD", commandType: CommandType.StoredProcedure);

            return result.ToList();
        }
        public bool AddCard(Card card)
        {
            var p = new DynamicParameters();
            p.Add("CCVCARD", card.Ccv, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Expdate", card.Expirededate, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("Bal", card.Balance, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            p.Add("Cardnum", card.Cardnumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Cardty", card.Cardtype, dbType: DbType.String, direction: ParameterDirection.Input);


            var result = DbContext.Connection.ExecuteAsync("CARD_F_PACKAGE.CREATETCARD", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return true;

        }

        public bool DeleteCard(int id)
        {
            var p = new DynamicParameters();
            p.Add("CarId", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = DbContext.Connection.ExecuteAsync("CARD_F_PACKAGE.DELETECARD", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return true;
        }

      

        public Card GetCardById(int Id)
        {

            var p = new DynamicParameters();
            p.Add("CarId", Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            var result = DbContext.Connection.QueryFirstOrDefault<Card>("CARD_F_PACKAGE.GETCARDBYID", p, commandType: CommandType.StoredProcedure);

            return result;

        }

        public bool UpdateCard(CardToUpdateDTO cardToUpdateDTO)
        {
            var p = new DynamicParameters();
            p.Add("CarId", cardToUpdateDTO.Cardid, dbType: DbType.Int32, direction: ParameterDirection.Input);

            p.Add("CCVCARD", cardToUpdateDTO.Ccv, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Expdate", cardToUpdateDTO.Expirededate, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("Bal", cardToUpdateDTO.Balance, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            p.Add("Cardnum", cardToUpdateDTO.Cardnumber, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Cardty", cardToUpdateDTO.Cardtype, dbType: DbType.String, direction: ParameterDirection.Input);


            var result = DbContext.Connection.ExecuteAsync("CARD_F_PACKAGE.UPDATERCARD", p, commandType: CommandType.StoredProcedure);
            if (result == null) return false;
            return true;
        }
    }
}
