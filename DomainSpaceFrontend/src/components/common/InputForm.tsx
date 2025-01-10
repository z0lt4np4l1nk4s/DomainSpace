import { FormEvent, ReactNode } from "react";

export default function InputForm(props: {
  onSubmit?: (event: FormEvent<HTMLFormElement>) => void;
  children?: ReactNode;
}) {
  return (
    <form onSubmit={props.onSubmit} className="form" noValidate={true}>
      {props.children}
    </form>
  );
}
