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
        get { return m_previousLayer; }
        protected set { m_previousLayer = value; }
    }

    private NeuronLayer m_nextLayer;
    public NeuronLayer NextLayer
    {
        get { return m_nextLayer; }
        set
        {
            m_nextLayer = value;
            // When changed NextLayer, need to update links between neurons in the two layers
            OnChanged