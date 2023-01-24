# Security concept

## New user-role concept:

![Security concept](https://raw.githubusercontent.com/Oh-my-class/oh-my-backend/develop/documentation/security_concept.png)


### Order of validation steps

1. Login/register requests have username/email and password in the body.
2. A unique session-bearer-token is created.
   1. Is invalid after a certain amount of time (session)
3. Every other request has a bearer token in the auth header
4. JWT are validated in JaasFilter.
5. JaasFilter gets user & roles from database.
6. User from database are saved into the subject in a principal.
7. Roles from user are saved into a subject in a principal.
8. Further, filters authenticate each role-principal for the method called and the rights from the database.
9. A response is formed.


### User-role-right structure of database
Validate for each URL called

    User: {
        ...,
        Role (MC:MC -> RoleAssignement)
    }

    RoleAssignement: {
        user : {...}, 
        role : {...}
    }

    Role: {
        id,
        code
    }

    RightAssignement: {
        role : {...},
        right : {...} 
    }

    Right: {
        id,
        code
    }

### URL protection

#### User
| URI                 | Required rights                                |
|:--------------------|:-----------------------------------------------|
| /login              | none                                           |
| /register           | none                                           |
| /get                | admin, can-read-user-details-of-specific-class |
| /update             | can-edit-own-details                           |
| /delete             | can-edit-own-details                           |
| /password-forgotten | can-read-edit-own-details                      |

#### Group (Class)

**Info**
* 'is-owner-of-class' -> automatically = 'is-member-of-class'
* 'is-owner-of-class' -> automatically = 'has-edit-class-member-rights'
* 'is-owner-of-class' -> automatically = 'has-edit-class-role-rights'

| URI            | Required rights                                                    |
|:---------------|:-------------------------------------------------------------------|
| /create        |  is-approved-user                                                  |
| /update        |  is-approved-user, is-owner-of-class                               |
| /get           |  is-approved-user, is-member-of-class                              |
| /delete        |  is-approved-user, is-owner-of-class                               |
| /add-member    |  is-approved-user, is-owner-of-class, has-edit-class-member-rights |
| /remove-member |  is-approved-user, is-owner-of-class, has-edit-class-member-rights |
| /add-role      |  is-approved-user, is-owner-of-class, has-edit-class-role-rights   |
| /get-role      |  is-approved-user, is-owner-of-class, has-edit-class-role-rights   |
| /update-role   |  is-approved-user, is-owner-of-class, has-edit-class-role-rights   |
| /remove-role   |  is-approved-user, is-owner-of-class, has-edit-class-role-rights   |
