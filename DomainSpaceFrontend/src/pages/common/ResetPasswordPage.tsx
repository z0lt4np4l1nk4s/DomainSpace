import React, { useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { AuthService } from "../../services";
import { PageRouteEnum, ResetPasswordModel } from "../../types";
import { ToastUtil } from "../../utils";
import {
  DefaultButton,
  InputForm,
  TextInput,
  NavBar,
  Loader,
  FooterComponent,
} from "../../components";

const authService = new AuthService();

export default function ResetPasswordPage() {
  const navigate = useNavigate();
  const location = useLocation();
  const params = useParams();
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitLoading, setIsSubmitLoading] = useState(false);
  const [isTokenValid, setIsTokenValid] = useState(false);
  const [formData, setFormData] = useState<ResetPasswordModel>({
    email: new URLSearchParams(location.search).get("email") || "",
    token: params.token,
    password: "",
    confirmPassword: "",
  });

  const [formValidations, setFormValidations] = useState({
    password: false,
    confirmPassword: false,
  });

  const handleInputChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ): void => {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value,
    });
  };

  const handleValidationChange = (name: string, isValid: boolean) => {
    setFormValidations((prevValidations) => ({
      ...prevValidations,
      [name]: isValid,
    }));
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const isFormValid = Object.values(formValidations).every(
      (isValid) => isValid
    );
    if (!isFormValid) {
      return;
    }

    if (formData.password !== formData.confirmPassword) {
      ToastUtil.showErrorMessage("Passwords do not match.");
      return;
    }

    setIsSubmitLoading(true);

    const response = await authService.resetPasswordAsync(formData);

    if (response.isSuccess) {
      ToastUtil.showSuccessMessage("Password reset successfully.");
      navigate(PageRouteEnum.Login);
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }

    setIsSubmitLoading(false);
  };

  useEffect(() => {
    const verifyTokenAsync = async () => {
      const response = await authService.verifyResetPasswordTokenAsync(
        formData.email!,
        formData.token!
      );

      if (response.isSuccess) {
        setIsTokenValid(true);
      }

      setIsLoading(false);
    };

    verifyTokenAsync();
  }, []);

  return (
    <div className="d-flex flex-column vh-100">
      <NavBar />
      <Loader isLoading={isLoading}>
        <div className="d-flex flex-grow-1 justify-content-center align-items-top mt-5">
          <div className="container mb-5">
            {!isTokenValid && (
              <div>
                <h2 className="text-center mb-4">Invalid token</h2>
              </div>
            )}
            {isTokenValid && (
              <div>
                <h2 className="text-center mb-4">Reset Password</h2>
                <div className="row justify-content-center">
                  <div className="col-md-6">
                    <InputForm onSubmit={handleSubmit}>
                      <div className="mb-3">
                        <TextInput
                          title="New Password"
                          type="password"
                          name="password"
                          value={formData.password}
                          onChange={handleInputChange}
                          onValidate={(value: string) => {
                            if (!value) {
                              return "Please enter your password.";
                            }

                            if (value.length < 8) {
                              return "Password must be at least 8 characters long.";
                            }

                            const passwordRegex =
                              /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?":{}|<>]).+$/;

                            if (!passwordRegex.test(value)) {
                              return "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
                            }
                          }}
                          onValidationChange={handleValidationChange}
                        />
                      </div>
                      <div className="mb-3">
                        <TextInput
                          title="Confirm Password"
                          type="password"
                          name="confirmPassword"
                          value={formData.confirmPassword}
                          onChange={handleInputChange}
                          onValidate={(value: string) => {
                            if (!value) {
                              return "Please confirm your password.";
                            }

                            if (value !== formData.password) {
                              return "Passwords do not match.";
                            }

                            return undefined;
                          }}
                          onValidationChange={handleValidationChange}
                        />
                      </div>

                      <div className="mt-5">
                        <DefaultButton
                          size="lg"
                          type="submit"
                          isLoading={isSubmitLoading}
                        >
                          Reset Password
                        </DefaultButton>
                      </div>
                    </InputForm>
                  </div>
                </div>
              </div>
            )}
          </div>
        </div>
      </Loader>
      <FooterComponent />
    </div>
  );
}
