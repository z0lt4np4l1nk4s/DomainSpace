import React, { useEffect, useState } from "react";
import { PageRouteEnum, RoleEnum } from "../../types";
import { Navigate } from "react-router-dom";
import NotFoundPage from "./NotFoundPage";
import { Loader } from "../../components";
import { AuthService, TokenService } from "../../services";

const authService = new AuthService();

export default function AuthPage({
  component: Component,
  roles,
  ...props
}: {
  component: React.ComponentType;
  roles?: RoleEnum[];
}) {
  const [isAuthenticated, setIsAuthenticated] = useState(true);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const checkAuthentication = async () => {
      const isAuthenticated = await authService.isAuthenticatedAsync();
      setIsAuthenticated(isAuthenticated);
      setIsLoading(false);
    };

    checkAuthentication();
  }, []);

  if (isLoading) {
    return (
      <Loader isLoading={isLoading}>
        <></>
      </Loader>
    );
  }

  const payload = TokenService.getUserPayload();

  if (
    !roles ||
    (roles &&
      payload &&
      payload.roles &&
      !roles.some((item) => payload.roles.includes(item)))
  ) {
    return <NotFoundPage />;
  }

  if (isAuthenticated) {
    return <Component {...props} />;
  }

  return (
    <Navigate
      to={PageRouteEnum.Login}
      state={{ from: window.location.pathname }}
      replace
    />
  );
}
