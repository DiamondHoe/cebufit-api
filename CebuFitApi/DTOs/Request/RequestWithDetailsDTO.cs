﻿using CebuFitApi.Helpers.Enums;

namespace CebuFitApi.DTOs
{
    public class RequestWithDetailsDto
    {
        public Guid Id { get; set; }
        public RequestType Type { get; set; }
        public UserPublicDto Requester { get; set; }
        public Guid RequestedItemId { get; set; }
        public RequestStatus Status { get; set; }
        public UserPublicDto? Approver { get; set; }
        public string? Description { get; set; }
    }
}

