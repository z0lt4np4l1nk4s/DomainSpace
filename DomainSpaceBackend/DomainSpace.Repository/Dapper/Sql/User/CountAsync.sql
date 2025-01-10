select 
	count(*)
from "AspNetUsers" u
inner join "AspNetUserRoles" ur on u."Id" = ur."UserId"
inner join "AspNetRoles" r on ur."RoleId" = r."Id"
/**where**/