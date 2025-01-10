import { useEffect, useState } from "react";
import {
  DefaultButton,
  FooterComponent,
  InputForm,
  Loader,
  NavBar,
  TextInput,
} from "../../components";
import { ChangePasswordModel, UpdateUserModel } from "../../types";
import { AuthService, UserDataService, UserService } from "../../services";
import { ToastUtil } from "../../utils";

const userService = new UserService();
const authService = new AuthService();

export default function AccountPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [profileFormData, setProfileFormData] = useState<UpdateUserModel>({});
  const [passwordFormData, setPasswordFormData] = useState<ChangePasswordModel>(
    {}
  );

  const handleProfileInputChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setProfileFormData({
      ...profileFormData,
      [event.target.name]: event.target.value,
    });
  };

  const handlePasswordInputChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setPasswordFormData({
      ...passwordFormData,
      [event.target.name]: event.target.value,
    });
  };

  const handleProfileUpdate = async (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    const result = await userService.updateAsync(
      UserDataService.getUserId()!,
      profileFormData
    );

    if (result.isSuccess) {
      ToastUtil.showSuccessMessage("Profile updated.");
    } else {
      ToastUtil.showErrorMessage(result.errorMessage?.description);
    }
  };

  const handlePasswordChange = async (
    event: React.FormEvent<HTMLFormElement>
  ) => {
    event.preventDefault();

    const result = await authService.changePasswordAsync(passwordFormData);

    if (result.isSuccess) {
      setPasswordFormData({
        oldPassword: "",
        newPassword: "",
        confirmNewPassword: "",
      });
      ToastUtil.showSuccessMessage("Password changed.");
    } else {
      ToastUtil.showErrorMessage(result.errorMessage?.description);
    }
  };

  useEffect(() => {
    const getUserByIdAsync = async () => {
      const response = await userService.getByIdAsync(
        UserDataService.getUserId() || ""
      );

      if (response.isSuccess) {
        setProfileFormData(response.result!);
      }

      setIsLoading(false);
    };

    getUserByIdAsync();
  }, []);

  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <Loader isLoading={isLoading}>
          <div className="container mt-5 mb-5">
            <div className="row justify-content-center">
              <div className="col-md-6">
                <h2 className="text-center mb-4">Account</h2>

                <InputForm onSubmit={handleProfileUpdate}>
                  <h5>Profile</h5>
                  <TextInput
                    name="firstName"
                    title="First name"
                    value={profileFormData.firstName}
                    onChange={handleProfileInputChange}
                  />
                  <TextInput
                    name="lastName"
                    title="Last name"
                    value={profileFormData.lastName}
                    onChange={handleProfileInputChange}
                  />
                  <DefaultButton width={180} type="submit">
                    {"Update info"}
                  </DefaultButton>
                </InputForm>
                <div className="mt-5">
                  <InputForm onSubmit={handlePasswordChange}>
                    <h5>{"Change password"}</h5>
                    <TextInput
                      name="oldPassword"
                      title="Old password"
                      type="password"
                      value={passwordFormData.oldPassword}
                      onChange={handlePasswordInputChange}
                    />
                    <TextInput
                      name="newPassword"
                      title="New password"
                      type="password"
                      value={passwordFormData.newPassword}
                      onChange={handlePasswordInputChange}
                    />
                    <TextInput
                      name="confirmNewPassword"
                      title="Confirm new password"
                      type="password"
                      value={passwordFormData.confirmNewPassword}
                      onChange={handlePasswordInputChange}
                    />
                    <DefaultButton width={180} type="submit">
                      {"Change password"}
                    </DefaultButton>
                  </InputForm>
                </div>
              </div>
            </div>
          </div>
        </Loader>
      </div>
      <FooterComponent />
    </div>
  );
}
