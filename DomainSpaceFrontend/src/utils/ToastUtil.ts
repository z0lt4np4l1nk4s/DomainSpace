import { toast } from "react-toastify";

export class ToastUtil {
  static showSuccessMessage(message: string) {
    toast.success(message, {
      position: "bottom-right",
      theme: "colored",
    });
  }

  static showErrorMessage(message?: string) {
    toast.error(message || "Something went wrong.", {
      position: "bottom-right",
      theme: "colored",
    });
  }

  static showWarningMessage(message: string) {
    toast.warning(message, {
      position: "bottom-right",
      theme: "colored",
    });
  }

  static showInfoMessage(message: string) {
    toast.info(message, {
      position: "bottom-right",
      theme: "colored",
    });
  }

  static showLoading(message: string) {
    toast.loading(message, {
      position: "bottom-right",
      theme: "colored",
      className: "bg-secondary text-white",
    });
  }

  static dismiss() {
    toast.dismiss();
  }
}
