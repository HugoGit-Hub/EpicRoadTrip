﻿using EpicRoadTrip.Application.Users;
using EpicRoadTrip.Domain.ErrorHandling;
using EpicRoadTrip.Domain.Users;
using EpicRoadTrip.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EpicRoadTrip.Infrastructure.Users;

public class UserRepository(EpicRoadTripContext context) : IUserRepository
{
    public async Task<Result<User>> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        
        return user is null 
            ? Result<User>.Failure(UserErrors.UserNotFoundByEmailError) 
            : Result<User>.Success(user);
    }
}