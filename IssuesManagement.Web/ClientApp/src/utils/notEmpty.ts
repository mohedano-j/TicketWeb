export const notEmpty = <TValue extends unknown>(value: TValue | null | undefined): value is TValue => {
  return value !== null && value !== undefined;
};
