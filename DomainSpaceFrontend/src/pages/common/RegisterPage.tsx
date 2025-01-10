import React, { useCallback, useState } from "react";
import { PageRouteEnum, RegisterModel } from "../../types";
import {
  DateInput,
  DefaultButton,
  FooterComponent,
  InputForm,
  NavBar,
  TextInput,
} from "../../components";
import { AuthService } from "../../services";
import { ToastUtil } from "../../utils";
import { useNavigate } from "react-router-dom";

const authService = new AuthService();

export default function RegisterPage() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState<RegisterModel>({});
  const [formValidations, setFormValidations] = useState({
    firstName: false,
    lastName: false,
    dateOfBirth: false,
    email: false,
    password: false,
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

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const response = await authService.registerAsync(formData);

    if (response.isSuccess) {
      ToastUtil.showSuccessMessage("Registration successful.");
      navigate(PageRouteEnum.Login);
    } else {
      ToastUtil.showErrorMessage(response.errorMessage?.description);
    }
  };

  return (
    <div className="d-flex flex-column vh-100">
      <NavBar />
      <div className="d-flex flex-grow-1 justify-content-center align-items-top mt-5">
        <div className="container">
          <h2 className="text-center mb-4">Register</h2>
          <div className="row justify-content-center">
            <div className="col-md-6">
              <InputForm onSubmit={handleSubmit}>
                <TextInput
                  title="First Name"
                  type="text"
                  name="firstName"
                  value={formData.firstName}
                  onChange={handleInputChange}
                  onValidate={(value: string) => {
                    if (!value) {
                      return "Please enter your first name.";
                    }
                    return undefined;
                  }}
                  onValidationChange={handleValidationChange}
                />

                <TextInput
                  title="Last Name"
                  type="text"
                  name="lastName"
                  value={formData.lastName}
                  onChange={handleInputChange}
                  onValidate={(value: string) => {
                    if (!value) {
                      return "Please enter your last name.";
                    }
                    return undefined;
                  }}
                  onValidationChange={handleValidationChange}
                />

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

                <TextInput
                  title="Password"
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

                    return undefined;
                  }}
                  onValidationChange={handleValidationChange}
                />

                <DateInput
                  title="Date of Birth"
                  name="dateOfBirth"
                  value={formData.dateOfBirth}
                  onChange={handleInputChange}
                  maxValue={new Date()}
                  onValidate={(value: string) => {
                    if (!value) {
                      return "Please select your date of birth.";
                    }

                    return undefined;
                  }}
                  onValidationChange={handleValidationChange}
                />

                <div className="mt-5">
                  <DefaultButton
                    size="lg"
                    type="submit"
                    disabled={
                      !Object.values(formValidations).every(
                        (isValid) => isValid
                      )
                    }
                  >
                    Register
                  </DefaultButton>
                </div>
              </InputForm>
              <p className="text-center mt-3">
                Already have an account?{"  "}
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
