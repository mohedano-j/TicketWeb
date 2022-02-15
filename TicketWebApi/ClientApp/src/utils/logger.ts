
export class logger {
  static debug = (message: string, values: any = {}) => {
    console.log({ message, values });
  };

  static error = (error: any) => {
    console.error(error);
  };
}
