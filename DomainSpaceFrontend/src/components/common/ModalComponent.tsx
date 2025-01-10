import { useRef } from "react";
import DefaultButton from "./DefaultButton";

export default function ModalComponent({
  title,
  children,
  show,
  width,
  onShowChange,
  onCancel,
  onConfirm,
}: {
  title: string;
  children: React.ReactNode;
  show: boolean;
  width?: number;
  onShowChange: (show: boolean) => void;
  onCancel?: () => void;
  onConfirm?: () => void;
}) {
  const modalRef = useRef<HTMLDivElement>(null);

  const handleClickOutside = (event: React.MouseEvent<HTMLDivElement>) => {
    if (modalRef.current && !modalRef.current.contains(event.target as Node)) {
      onShowChange(false);
    }
  };

  return (
    <div>
      {show && (
        <div
          className="modal d-flex align-items-center justify-content-center"
          tabIndex={-1}
          style={{
            display: "block",
            backgroundColor: "rgba(0, 0, 0, 0.5)",
          }}
          onClick={handleClickOutside}
        >
          <div
            className="modal-dialog"
            style={{
              width: "100%",
              maxWidth: width ? `${width}px` : "450px",
              padding: "0 16px",
              boxSizing: "border-box",
            }}
          >
            <div className="modal-content" ref={modalRef}>
              <div className="modal-header">
                <h5 className="modal-title">{title}</h5>
              </div>
              <div className="modal-body">{children}</div>
              <div className="modal-footer">
                <DefaultButton
                  color="secondary"
                  onClick={() => {
                    if (onCancel) {
                      onCancel();
                    }
                    onShowChange(false);
                  }}
                >
                  {"Close"}
                </DefaultButton>
                <DefaultButton
                  onClick={() => {
                    if (onConfirm) {
                      onConfirm();
                    }
                    onShowChange(false);
                  }}
                  color="success"
                >
                  {"Confirm"}
                </DefaultButton>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
