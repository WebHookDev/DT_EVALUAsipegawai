public class NeuronLayer
{
    private NeuralNetwork m_neuralNetwork;

    #region Neurons
    private int m_neuronNb;
    public int NeuronNb
    {
        get { return m_neuronNb; }
        protected set { m_neuronNb = value; }
    }

    private Neuron[] m_neurons;
    public Neuron[] Neurons
    {
        get { return m_neurons; }
        protected set { m_neurons = value; }
    }
    #endregion

    #region Linked layers
    private NeuronLayer m_previousLayer;
    public NeuronLayer PreviousLayer
    {
        get { return m_