using EventReservation.Core.Data;
using EventReservation.Core.DTO;
using EventReservation.Core.Repository;
using EventReservation.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReservation.Infra.Service
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public bool AddCard(Card card)
        {
            return _cardRepository.AddCard(card);
        }

        public bool DeleteCard(int id)
        {
            return _cardRepository.DeleteCard(id);
        }

        public List<Card> GetAllCards()
        {
            return _cardRepository.GetAllCards();
        }

        public Card GetCardById(int Id)
        {
            return _cardRepository.GetCardById(Id);
        }

        public bool UpdateCard(CardToUpdateDTO cardToUpdateDTO)
        {
            return _cardRepository.UpdateCard(cardToUpdateDTO);
        }
    }
}
