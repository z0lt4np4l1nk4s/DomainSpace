import { ReactNode } from "react";

export default function TableComponent({ children }: { children: ReactNode }) {
  return (
    <div className="table-responsive">
      <table className="table table-hover table-striped">{children}</table>
    </div>
  );
}
