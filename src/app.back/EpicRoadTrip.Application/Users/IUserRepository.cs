﻿using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;

namespace EpicRoadTrip.Application.Users;

public interface IUserRepository
{
    public Task<Result<User>> Create(User user, CancellationToken cancellationToken);
}