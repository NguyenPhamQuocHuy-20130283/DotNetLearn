using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Tittle = comment.Tittle,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }
        public static Comment ToCommentFromCreateCommentDto(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Tittle = commentDto.Tittle,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
        public static Comment ToCommentFromUpdateCommentDto(this UpdateCommentDto commentDto)
        {
            return new Comment
            {
                Tittle = commentDto.Tittle,
                Content = commentDto.Content
            };
        }
    }
}