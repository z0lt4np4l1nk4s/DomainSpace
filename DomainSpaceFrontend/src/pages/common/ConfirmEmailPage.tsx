import { useEffect, useState } from "react";
import { BasicLoader, FooterComponent, NavBar } from "../../components";
import { AuthService } from "../../services";
import { useParams } from "react-router-dom";
import { ToastUtil } from "../../utils";

const authService = new AuthService();

export default function ConfirmEmailPage() {
  const [isLoading, setIsLoading] = useState(true);
  const [text, setText] = useState("Confirming email...");
  const param = useParams();

  useEffect(() => {
    const confirmEmailAsync = async () => {
      const result = await authService.confirmEmailAsync(param.token ?? "");

      if (result.isSuccess) {
        setText("Email confirmed");
        ToastUtil.showSuccessMessage("Email confirmed");
      } else {
        setText("Email confirmation failed");
        ToastUtil.showErrorMessage(result.errorMessage?.description);
      }

      setIsLoading(false);
    };

    confirmEmailAsync();
  }, [param.token]);

  return (
    <div className="d-flex flex-column vh-100">
      <div className="flex-grow-1">
        <NavBar />
        <div className="container mt-5 d-flex flex-column align-items-center">
          {isLoading ? (
            <div className="d-flex flex-column align-items-center mt-5">
              <BasicLoader />
              <h2>{text}</h2>
            </div>
          ) : (
            <div className="mt-5">
              <h2>{text}</h2>
            </div>
          )}
        </div>
      </div>
      <FooterComponent />
    </div>
  );
}
