import ReactDOM from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import {
  AccountPage,
  AuthPage,
  ConfirmEmailPage,
  ContentDetailsPage,
  ContentEditPage,
  ContentPublishPage,
  ForgotPasswordPage,
  HomePage,
  LoginPage,
  NotFoundPage,
  RegisterPage,
  ResetPasswordPage,
  SubjectsPage,
  UsersPage,
} from "./pages";
import {
  AllRoleEnums,
  ModeratorRoleEnums,
  PageRouteEnum,
  RoleEnum,
} from "./types";

const router = createBrowserRouter([
  {
    path: PageRouteEnum.Home,
    element: <HomePage />,
  },
  {
    path: PageRouteEnum.Login,
    element: <LoginPage />,
  },
  {
    path: PageRouteEnum.Register,
    element: <RegisterPage />,
  },
  {
    path: PageRouteEnum.Account,
    element: <AuthPage component={AccountPage} roles={AllRoleEnums} />,
  },
  {
    path: PageRouteEnum.Subjects,
    element: <AuthPage component={SubjectsPage} roles={ModeratorRoleEnums} />,
  },
  {
    path: PageRouteEnum.Users,
    element: <AuthPage component={UsersPage} roles={[RoleEnum.Admin]} />,
  },

  // Content
  {
    path: PageRouteEnum.PublishContent,
    element: <AuthPage component={ContentPublishPage} roles={AllRoleEnums} />,
  },
  {
    path: PageRouteEnum.EditContent,
    element: <AuthPage component={ContentEditPage} roles={AllRoleEnums} />,
  },
  {
    path: PageRouteEnum.ContentDetails,
    element: <AuthPage component={ContentDetailsPage} roles={AllRoleEnums} />,
  },

  {
    path: PageRouteEnum.ConfirmEmail,
    element: <ConfirmEmailPage />,
  },
  {
    path: PageRouteEnum.ForgotPassword,
    element: <ForgotPasswordPage />,
  },
  {
    path: PageRouteEnum.ResetPassword,
    element: <ResetPasswordPage />,
  },

  // Not Found
  {
    path: "*",
    element: <NotFoundPage />,
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <>
    <ToastContainer autoClose={5000} aria-label="Notification container" />
    <RouterProvider router={router} />
  </>
);
