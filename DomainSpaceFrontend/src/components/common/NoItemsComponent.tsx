export default function NoItemsComponent({ width }: { width?: string }) {
  return (
    <div className="d-flex justify-content-center align-items-center flex-column mt-3 mb-3">
      <img
        src="/assets/images/no_items.png"
        alt="No items"
        style={{
          width: width || "100%",
          maxWidth: "256px",
          maxHeight: "256px",
        }}
      />
      <h4>{"No items"}</h4>
    </div>
  );
}
