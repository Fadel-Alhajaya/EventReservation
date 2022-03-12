using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Core.Service
{
    public interface ICardService
    {
        List<Card> GetAllCards();
        //Update Update()

        bool AddCard(Card card);
        bool UpdateCard(CardToUpdateDTO cardToUpdateDTO);

        bool DeleteCard(int id);

        Card GetCardById(int Id);
    }
}
