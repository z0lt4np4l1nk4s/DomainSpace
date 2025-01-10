import { useCallback, useState } from "react";
import {
  DefaultButton,
  FooterComponent,
  InputForm,
  NavBar,
  TextInput,
} from "../../components";
import { PageRouteEnum } from "../../types";
import { AuthService } from "../../services";
import { ToastUtil } from "../../utils";

const authService = new AuthService();

export default function ForgotPasswordPage() {
  const [formData, setFormData] = useState({
    email: "",
  });

  const [formValidations, setFormValidations] = useState({
    email: true,
  });

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [event.target.name]: event.target.value,
    });
  };

  const handleValidationChange = useCallback(
    (name: string, isValid: boolean) => {
      setFormValidations((prevValidations) => ({
        ...prevValidations,
        [name]: isValid,
      }));
    },
    []
  );

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const isFormValid = Object.values(formValidations).every(
      (isValid) => isValid
    );

    if (!isFormValid) {
      ToastUtil.showErrorMessage("Please fix the errors in the form.");
      return;
    }

    const result = await authService.sendResetPasswordEmailAsync(
      formData.email
    );

    if (result.isSuccess) {
      ToastUtil.showSuccessMessage(
        "A password reset email has been sent. Please check your inbox."
      );
    } else {
      ToastUtil.showErrorMessage(result.errorMessage?.description);
    }
  };

  return (
    <div className="d-flex flex-column vh-100">
      <NavBar />
      <div className="d-flex flex-grow-1 justify-content-center align-items-top mt-5">
        <div className="container mb-5">
          <h2 className="text-center mb-4">Forgot Password</h2>
          <div className="row justify-content-center">
            <div className="col-md-6">
              <InputForm onSubmit={handleSubmit}>
                <div className="mb-3">
                  <TextInput
                    title="Email"
                    type="email"
                    name="email"
                    value={formData.email}
                    onChange={handleInputChange}
                    onValidate={(value: string) => {
                      if (!value) {
                        return "Please enter your email address.";
                      }

                      const emailRegex =
                        /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
                      if (!emailRegex.test(value)) {
                        return "Please enter a valid email address.";
                      }

                      return undefined;
                    }}
                    onValidationChange={handleValidationChange}
                  />
                </div>

                <div className="mt-5">
                  <DefaultButton size="lg" type="submit">
                    Send Reset Email
                  </DefaultButton>
                </div>
              </InputForm>

              <p className="text-center mt-3">
                Remembered your password?{"  "}
                <a href={PageRouteEnum.Login} className="text-primary">
                  Login
                </a>
              </p>
            </div>
          </div>
        </div>
      </div>
      <FooterComponent />
    </div>
  );
}
