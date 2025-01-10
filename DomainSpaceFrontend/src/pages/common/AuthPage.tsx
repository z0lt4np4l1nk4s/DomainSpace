import React, { useEffect, useState } from "react";
import { PageRouteEnum, RoleEnum } from "../../types";
import { Navigate } from "react-router-dom";
import NotFoundPage from "./NotFoundPage";
import { Loader } from "../../components";
import { AuthService, UserDataService } from "../../services";

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

  const userToken = UserDataService.getUserToken();

  if (
    !roles ||
    (roles &&
      userToken &&
      userToken.roles &&
      !roles.some((item) => userToken.roles.includes(item)))
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
