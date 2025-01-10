select
	-- User
	u."Id",
	u."FirstName",
	u."LastName",
	u."Email",
	
	-- Role
	r."Name" as "RoleName"
from "AspNetUsers" u
inner join "AspNetUserRoles" ur on u."Id" = ur."UserId"
inner join "AspNetRoles" r on ur."RoleId" = r."Id"
/**where**/
/**orderby**/
limit @pageSize
offset @offset